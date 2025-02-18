using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MickesVäder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Collection<WeatherData> weatherData = ReadWeatherFile.ReadAll("tempdata5-med fel.txt");

            WriteWeatherStatsFile.SaveMonthlyAverages(weatherData);

            while (true)
            {
                Display.Menu(weatherData); 
            }
        }
    }
}
