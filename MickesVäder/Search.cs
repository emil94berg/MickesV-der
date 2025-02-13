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
        
        public static void DisplayAvg(Collection<WeatherData> weather, string date)
        {
            var test = (from x in weather
                        where x.Date.ToString().Contains(date)
                        group x by x.Date into days
                        select new
                        {
                            AvgTemperature = Math.Round(days.Average(x => x.Temp), 2),
                            AvgDate = days.Key,
                            AvgMoist = Math.Round(days.Average(x => x.Moist), 0),
                            Mold = ((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp)/15)/0.22), 0) : 0
                            //((luftfuktighet -78) * (Temp/15))/0,22

                        });
            
            foreach(var testWeather in test)
            {
                Console.WriteLine("Date: " + testWeather.AvgDate + "\tAvg temp: " + (testWeather.AvgTemperature).ToString() + "\tAvg humidity: " + testWeather.AvgMoist + " mold: " + testWeather.Mold);
            }
            if (test.Count() > 1)
            {
                double avgTempMonth = 0;
                foreach(var testWeather1 in test)
                {
                    avgTempMonth += testWeather1.AvgTemperature;
                }
                Console.WriteLine("Medeltemp på månad = " + Math.Round(avgTempMonth / test.Count(), 2)); 
                
                
                                   
            }
        }

        public static void AutumnWinter(Collection<WeatherData> weather, bool winter)
        {
            DateOnly firstDate = new DateOnly(2016,08,01);
            DateOnly lastDate = new DateOnly(2017,02,14);
            var autumn = weather.Where(x => (x.Date >= firstDate) && (x.Date <= lastDate)).ToList();

            autumn = autumn.OrderBy(w => w.Date).ToList();

            //Använda timespan för att kolla så att dagarna ligger efter varandra
            int consecutiveDays = 0;
            const int requiredDays = 5; // Antal dagar i rad som krävs
            DateOnly? startDate = null; // Sparar startdatumet för perioden
            DateOnly? endDate = null;   // Sparar slutdatumet för perioden
            DateOnly yesterday = firstDate;
            int lowerstTemp = 0;
            int higestTemp = 10;

            if(winter == true)
            {
                lowerstTemp = -273;
                higestTemp = 0;
            }

            
            foreach (var testWeather in autumn)
            {
                var span = testWeather.Date.DayNumber - yesterday.DayNumber;
                if (testWeather.Temp > lowerstTemp && testWeather.Temp < higestTemp && span <= 1)
                {
                    if (consecutiveDays == 0)
                    {
                        startDate = testWeather.Date; // Spara startdatumet när en ny period börjar
                    }

                    consecutiveDays++; // Öka räknaren
                    
                    if (consecutiveDays == requiredDays)
                    {
                        endDate = testWeather.Date; // Spara slutdatumet
                        Console.WriteLine($"Hittade en period på {requiredDays} dagar i rad där temperaturen är mellan {lowerstTemp} och {higestTemp} grader.");
                        Console.WriteLine($"Startdatum: {startDate?.ToShortDateString()}, Slutdatum: {endDate?.ToShortDateString()}");
                        break; // Avsluta loopen när vi har hittat en matchning
                    }
                }
                else
                {
                    consecutiveDays = 0; // Nollställ om en dag bryter kedjan
                    startDate = null; // Återställ startdatum
                }
                yesterday = testWeather.Date;
            }

            if (consecutiveDays < requiredDays)
            {
                Console.WriteLine("Ingen sådan period hittades.");
            }
        }

        
    }
}
