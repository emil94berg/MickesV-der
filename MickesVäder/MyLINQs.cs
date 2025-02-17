using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class MyLINQs
    {
        public static IEnumerable<WeatherData> SortByDayFunc(Collection<WeatherData> weather)
        {
            var test = (from x in weather
                            //where x.Location == location
                        group x by new { x.Date, x.Location } into days
                        select new WeatherData
                        {
                            Temp = Math.Round(days.Average(x => x.Temp), 2),
                            Date = new DateOnly(days.Key.Date.Year, days.Key.Date.Month, days.Key.Date.Day),
                            Moist = Math.Round(days.Average(x => x.Moist), 0),
                            Mold = ((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp) / 15) / 0.22), 0) : 0,
                            Location = days.Key.Location

                            //((luftfuktighet -78) * (Temp/15))/0,22

                        });
            return test;
        }

        public static IEnumerable<WeatherData> SortByMonthFunc(Collection<WeatherData> weather)
        {
            var test = (from x in weather
                            //where x.Location == location
                        group x by new {x.Date.Year, x.Date.Month, x.Location} into days
                        select new WeatherData
                        {
                            Temp = Math.Round(days.Average(x => x.Temp), 2),
                            Date = new DateOnly(days.Key.Year, days.Key.Month, 1),
                            Moist = Math.Round(days.Average(x => x.Moist), 0),
                            Mold = ((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp) / 15) / 0.22), 0) : 0,
                            Location = days.Key.Location

                            //((luftfuktighet -78) * (Temp/15))/0,22

                        });
            return test;
        }
    }
}
