using System.Collections.ObjectModel;

namespace MickesVäder
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Collection<WeatherData> data = ReadWeatherFile.ReadAll("tempdata5-med fel.txt", "Ute");
            //foreach (WeatherData weather in data)
            //{
            //    Console.WriteLine(weather.Date + " " + weather.Temp);
            //}
            Search.DisplayAvg(data, "2016");

        }
    }
}
