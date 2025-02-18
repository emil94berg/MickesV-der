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
        public static Collection<WeatherData> ReadAll(string fileName)
        {
            Collection<WeatherData> weatherLines = new Collection<WeatherData>();
            string[] lines = File.ReadAllLines(Path + fileName);
            Regex regex = new Regex("^(?<year>\\d{4})-(?<month>0[1-9]|1[0-2])-(?<day>0[1-9]|[1-2][0-9]|3[0-1])\\W(?<time>([0-1][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]),(?<location>Inne|Ute),(?<temp>(\\b|\\-)\\d{1,2}\\.\\d),(?<moist>\\d{1,3})$");
            List<string> errorList = new List<string>();

            foreach (var line in lines)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    var newWeather = new WeatherData
                    {
                        Date = new DateOnly(int.Parse(match.Groups["year"].Value), int.Parse(match.Groups["month"].Value), int.Parse(match.Groups["day"].Value)),
                        Time = match.Groups["time"].Value,
                        Location = match.Groups["location"].Value,
                        Temp = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture),
                        Moist = int.Parse(match.Groups["moist"].Value)
                    };
                    weatherLines.Add(newWeather);
                } else if (!match.Success) {
                    errorList.Add(line);
                }
            }

            errorList.SaveToFile("DataErrorLog.txt");

            return weatherLines;
        }
    }
}
