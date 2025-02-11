using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class Sorting
    {
        public static List<string> GetAllOnLocation (string[] dataSet, string location)
        {
            List<string> result = new List<string>();
            //grupp 5 inne/ute
            Regex regex = new Regex("^(?<year>\\d{4})-(?<month>\\d{2})-(?<day>\\d{2})\\W(?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>(\\b|\\-)\\d{1,2}\\.\\d),(?<moist>\\d{1,3})$");
            
            foreach (var line in dataSet)
            {
                Match match = regex.Match(line);
                if (match.Success && (match.Groups["location"].Value == location))
                {
                    result.Add(line);
                    
                }
            }
            return result;  
        }
        
    }
}
