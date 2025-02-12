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
                        where x.Date.Contains(date)
                        group x by x.Date into days
                        select new
                        {
                            AvgTemperature = Math.Round(days.Average(x => x.Temp), 2),
                            AvgDate = days.Key,
                            AvgMoist = Math.Round(days.Average(x => x.Moist), 0),
                            Mold = ((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp)/15)/0.22), 0) : 0
                            //((luftfuktighet -78) * (Temp/15))/0,22

                        }).OrderByDescending(x => x.AvgTemperature);
            
            foreach(var testWeather in test)
            {
                Console.WriteLine("Date: " + testWeather.AvgDate + "\tAvg temp: " + (testWeather.AvgTemperature).ToString() + "\tAvg humidity: " + testWeather.AvgMoist + " mold: " + testWeather.Mold);
            }
        }

        public static void Mold(Collection<WeatherData> weather, string date)
        {

        }

        
    }
}
