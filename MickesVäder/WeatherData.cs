using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class WeatherData
    {
        public DateOnly Date { get; set; } = new DateOnly();
        public string? Time { get; set; }
        public string Location { get; set; }
        public double Temp { get; set; }
        public double Moist { get; set; }
        public double? Mold { get; set; }

        //public WeatherData(int year, int month, int day, string time, string location, double temp, double moist, double mold)
        //{
        //    Date = new DateOnly(year, month, day);
        //    Time = time;    
        //    Location = location;
        //    Temp = temp;
        //    Moist = moist;
        //    Mold = mold;

        //}
    }
}
