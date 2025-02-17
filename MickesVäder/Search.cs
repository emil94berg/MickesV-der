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
        
        public static Collection<WeatherData> CalculateAvg(Collection<WeatherData> weather)
        {
            
            var test = (from x in weather
                        //where x.Location == location
                        group x by new { x.Date, x.Location } into days
                        select new WeatherData
                        {
                            Temp = Math.Round(days.Average(x => x.Temp), 2),
                            Date = new DateOnly(days.Key.Date.Year, days.Key.Date.Month, days.Key.Date.Day),
                            Moist = Math.Round(days.Average(x => x.Moist), 0),
                            Mold = ((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp)/15)/0.22), 0) : 0,
                            Location = days.Key.Location                            
                            
                            //((luftfuktighet -78) * (Temp/15))/0,22

                        });
            Collection<WeatherData> results = new Collection<WeatherData>();
            foreach (var testWeather in test)
            {
                //Console.WriteLine("Date: " + testWeather.AvgDate + "\tAvg temp: " + (testWeather.AvgTemperature).ToString() + "\tAvg humidity: " + testWeather.AvgMoist + " mold: " + testWeather.Mold);
                results.Add(testWeather);
                //Collection<WeatherData> avgList = new WeatherData{int.Parse(testWeather.AvgDate.Year), int.Parse(testWeather.AvgDate.Month), int.Parse(testWeather.AvgDate.Day), double.Parse(testWeather.AvgTemperature), weather[0].Location }
            }
            //if (test.Count() > 1)
            //{
            //    double avgTempMonth = 0;
            //    string datee = "";
            //    foreach (var testWeather1 in test)
            //    {
            //        avgTempMonth += testWeather1.AvgTemperature;
            //         datee = testWeather1.AvgDate.ToString();
            //    }
            //    Console.WriteLine("Medeltemp på månad = " + Math.Round(avgTempMonth / test.Count(), 2) + " " + datee);

            //}
            return results;
        }

        public static string AutumnWinter(Collection<WeatherData> weather,int lowerstTemp, int higestTemp)
        {
            DateOnly firstDate = new DateOnly(2016,08,01);
            DateOnly lastDate = new DateOnly(2017,02,14);
            var autumn = weather.Where(x => (x.Date >= firstDate) && (x.Date <= lastDate)).ToList();

            autumn = autumn.OrderBy(w => w.Date).ToList();

            //Använda timespan för att kolla så att dagarna ligger efter varandra
            int consecutiveDays = 0;
            const int requiredDays = 5; // Antal dagar i rad som krävs
            int closestPeriod = 0;
            DateOnly? closestDate = null;
            DateOnly? startDate = null; // Sparar startdatumet för perioden
            DateOnly? endDate = null;   // Sparar slutdatumet för perioden
            DateOnly yesterday = firstDate;
            //int lowerstTemp = 0;
            //int higestTemp = 0;
            //Console.WriteLine("[1]Vinter [2]Höst ");
            //var userNumber = Console.ReadKey(true);

            //if(userNumber.KeyChar == '1')
            //{
            //    lowerstTemp = -273;
            //    higestTemp = 0;
            //}
            //else if (userNumber.KeyChar == '2')
            //{
            //    lowerstTemp = 0;
            //    higestTemp = 10;
            //}
            string? saveDate = null;
            
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
                        saveDate = $"Startdatum: {startDate?.ToShortDateString()}, Slutdatum: {endDate?.ToShortDateString()}";
                        break; // Avsluta loopen när vi har hittat en matchning
                    }
                }
                else
                {
                    if(consecutiveDays > closestPeriod)
                    {
                        closestPeriod = consecutiveDays;
                        closestDate = startDate;
                    }
                    consecutiveDays = 0; // Nollställ om en dag bryter kedjan
                    startDate = null; // Återställ startdatum
                    
                }
                yesterday = testWeather.Date;
            }

            if (consecutiveDays < requiredDays)
            {
                Console.WriteLine("Ingen sådan period hittades.");
                Console.WriteLine("Närmast var: " + closestDate + " antal dagar: " + closestPeriod);
                saveDate = "Närmast var: " + closestDate + " antal dagar: " + closestPeriod;
            }
            return saveDate;
        }
        
        
    }
}
