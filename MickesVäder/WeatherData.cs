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

    }
}
