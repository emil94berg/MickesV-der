using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class GetValues
    {
        public static void Daily (Collection<WeatherData> data, string insideOutside, string userAnswer)
        {
            Regex regex = new Regex("(?<year>\\d{4}).(?<month>0[1-9]|1[0-2]).(?<day>0[1-9]|[1-2][0-9]|3[0-1])");
            Match match = regex.Match(userAnswer);
            Regex regex1 = new Regex("^(?<year>\\d{4}).(?<month>0[1-9]|1[0-2])$");
            Match match1 = regex1.Match(userAnswer);

            if (match1.Success)
            {
                Display.DataSet(data.Where(x => x.Date.ToString().Contains(match1.Groups["year"].Value + "-" + match1.Groups["month"].Value) && x.Location == insideOutside));
            }
            else if (match.Success)
            {
                DateOnly checkDate = new DateOnly(int.Parse(match.Groups["year"].Value), int.Parse(match.Groups["month"].Value), int.Parse(match.Groups["day"].Value));

                Display.DataSet(data.Where(x => x.Date == checkDate && x.Location == insideOutside));
            }
            else
            {
                Console.WriteLine("Felaktigt datum");
            }
        }
    }
}
