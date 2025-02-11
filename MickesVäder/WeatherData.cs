using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class WeatherData
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public double Temp { get; set; }
        public int Moist { get; set; }

        public WeatherData(string date, string time, string location, double temp, int moist)
        {
            Date = date;
            Time = time;    
            Location = location;
            Temp = temp;
            Moist = moist;

        }
    }
}
