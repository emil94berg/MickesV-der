using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MickesVäder
{
    internal class ReadWeatherFile
    {
        public static string Path = "../../../Files/";

        public static Collection<WeatherData> ReadAll(string fileName, string location)
        {
            Collection<WeatherData> weatherLines = new Collection<WeatherData>();
            string[] lines = File.ReadAllLines(Path + fileName);
            Regex regex = new Regex("^(?<date>\\d{4}-\\d{2}-\\d{2})\\W(?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>(\\b|\\-)\\d{1,2}\\.\\d),(?<moist>\\d{1,3})$");
            

            foreach (var line in lines)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    weatherLines.Add(new WeatherData(match.Groups["date"].Value, match.Groups["time"].Value, match.Groups["location"].Value, double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture), int.Parse(match.Groups["moist"].Value)));
                }
            }
            var test = (from x in weatherLines
                        where x.Location == location
                        group x by x.Date into days
                        select new
                        {
                            AvgTemperature = Math.Round(days.Average(x => x.Temp), 2),
                            AvgDate = days.Key,
                            AvgMoist = Math.Round(days.Average(x => x.Moist), 0)

                        });
            Collection<WeatherData> newLines = new Collection<WeatherData>();
            foreach (var line in test)
            {
                newLines.Add(new WeatherData(line.AvgDate,null, location, line.AvgTemperature, line.AvgMoist));
            }

            return newLines;
        }
    }
}
