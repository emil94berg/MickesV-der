using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    
    internal static class WriteWeatherStatsFile
    {
        public static string path = "../../../Files/";
        public static void SaveToFile(this string text)
        {
            using (StreamWriter streamwrite = new StreamWriter(path + "WeatherStats.txt", true))
            {
                streamwrite.WriteLine(text);
            }
        }
        public static void SaveMonthlyAverages()
        {

        } 
    }
}
