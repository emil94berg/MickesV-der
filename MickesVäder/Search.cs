using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MickesVäder
{
    internal class Search
    {
        
        public static Collection<WeatherData> CalculateAvg(Collection<WeatherData> weather, bool dateCheck)
        {
            Func<Collection<WeatherData>, IEnumerable<WeatherData>> sortedData = dateCheck ? MyLINQs.SortByDay : MyLINQs.SortByMonth;
            Collection<WeatherData> results = new Collection<WeatherData>();

            foreach (var testWeather in sortedData(weather))
            {
                results.Add(testWeather);
            }

            return results;
        }

        public static string AutumnWinter(Collection<WeatherData> weather,int lowerstTemp, int higestTemp)
        {
            DateOnly firstDate = new DateOnly(2016,08,01);
            DateOnly lastDate = new DateOnly(2017,02,14);

            var autumn = weather.Where(x => (x.Date >= firstDate) && (x.Date <= lastDate) && x.Location == "Ute")
                                .OrderBy(w => w.Date)
                                .ToList();

            int consecutiveDays = 0;
            const int requiredDays = 5; 
            int closestPeriod = 0;
            DateOnly? closestDate = null;
            DateOnly? startDate = null; 
            DateOnly? endDate = null;   
            DateOnly yesterday = firstDate;

            string? saveDate = null;
            
            foreach (var testWeather in autumn)
            {
                var span = testWeather.Date.DayNumber - yesterday.DayNumber;
                if (testWeather.Temp > lowerstTemp && testWeather.Temp < higestTemp && span <= 1)
                {
                    if (consecutiveDays == 0)
                    {
                        startDate = testWeather.Date;
                    }

                    consecutiveDays++;
                    
                    if (consecutiveDays == requiredDays)
                    {
                        endDate = testWeather.Date;
                        saveDate = $"Startdatum: {startDate?.ToShortDateString()}, Slutdatum: {endDate?.ToShortDateString()}";

                        break;
                    }
                }
                else
                {
                    if(consecutiveDays > closestPeriod)
                    {
                        closestPeriod = consecutiveDays;
                        closestDate = startDate;
                    }
                    consecutiveDays = 0;
                    startDate = null;
                    
                }
                yesterday = testWeather.Date;
            }

            if (consecutiveDays < requiredDays)
            {
                saveDate = "Närmast var: " + closestDate + " antal dagar: " + closestPeriod;
            }
            return saveDate;
        }
        
        
    }
}
