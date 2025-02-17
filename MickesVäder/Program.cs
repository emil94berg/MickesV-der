using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MickesVäder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Show.Menu();
            //Collection<WeatherData> dataOutside = ReadWeatherFile.ReadAll("tempdata5-med fel.txt", "Ute");
            //Collection<WeatherData> dataInside = ReadWeatherFile.ReadAll("tempdata5-med fel.txt", "Inne");
            //Collection<WeatherData> avgData = Search.CalculateAvg(dataOutside);
            //foreach (WeatherData weather in data)
            //{
            //    Console.WriteLine(weather.Date + " " + weather.Temp);
            //}
            //Search.DisplayAvg(data, "2016-11");
            //Search.AutumnWinter(data, true);
            //Sorting.GetDailyValues(avgData);
            //Sorting.GetMonthlyValues(avgData);
            //Sorting.SortValuesBy(avgData);

        }
    }
}
