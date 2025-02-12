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
        }

        public static void AutumnWinter(Collection<WeatherData> weather, string date)
        {
            DateOnly startDate = new DateOnly(2016,08,01);
            DateOnly endDate = new DateOnly(2017,02,14);
            int countDay = 0;
            var startTemp = 0;
            var endTemp = 0;
            var preTemp = 0;

            var autumn = weather.Where(x => (x.Date >= startDate) && (x.Date <= endDate)).ToList();

            autumn = autumn.OrderBy(w => w.Date).ToList();




            //Använda timespan för att kolla så att dagarna ligger efter varandra
            //int consecutiveDays = 0;
            //const int requiredDays = 5; // Antal dagar i rad som krävs
            //DateTime? startDate = null; // Sparar startdatumet för perioden
            //DateTime? endDate = null;   // Sparar slutdatumet för perioden


            
            //foreach (var testWeather in autumn)
            //{
            //    if (testWeather.Temp > 0 && testWeather.Temp < 10)
            //    {
            //        if (consecutiveDays == 0)
            //        {
            //            startDate = testWeather.Date; // Spara startdatumet när en ny period börjar
            //        }

            //        consecutiveDays++; // Öka räknaren

            //        if (consecutiveDays == requiredDays)
            //        {
            //            endDate = testWeather.Date; // Spara slutdatumet
            //            Console.WriteLine($"Hittade en period på {requiredDays} dagar i rad där temperaturen är mellan 0 och 10 grader.");
            //            Console.WriteLine($"Startdatum: {startDate?.ToShortDateString()}, Slutdatum: {endDate?.ToShortDateString()}");
            //            break; // Avsluta loopen när vi har hittat en matchning
            //        }
            //    }
            //    else
            //    {
            //        consecutiveDays = 0; // Nollställ om en dag bryter kedjan
            //        startDate = null; // Återställ startdatum
            //    }
            //}

            //if (consecutiveDays < requiredDays)
            //{
            //    Console.WriteLine("Ingen sådan period hittades.");
            //}
        }

        
    }
}
