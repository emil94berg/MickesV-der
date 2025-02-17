using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class Show
    {
        public static void Menu()
        {
            //Collection<WeatherData> dataOutside = ReadWeatherFile.ReadAll("tempdata5-med fel.txt", "Ute");
            //Collection<WeatherData> avgDataOutside = Search.CalculateAvg(dataOutside, "Ute");
            //Collection<WeatherData> dataInside = ReadWeatherFile.ReadAll("tempdata5-med fel.txt", "Inne");
            //Collection<WeatherData> avgDataInside = Search.CalculateAvg(dataInside, "Inne");
            Collection<WeatherData> weatherDatas = ReadWeatherFile.ReadAll("tempdata5-med fel.txt");
            Collection<WeatherData> avgData = Search.CalculateAvg(weatherDatas);
            Console.WriteLine("[1] Innomhus [2] Utomhus");
            var userNumber = Console.ReadKey(true);
            //Collection<WeatherData> insideOutside = new Collection<WeatherData>();
            string insideOutside = "";
            if(userNumber.KeyChar == '1')
            {
                insideOutside = "Inne";
            }
            else if(userNumber.KeyChar == '2')
            {
                insideOutside = "Ute";
            }
            
            Console.WriteLine("[1] Sortera meny [2] Dagliga/Månadsvis medel temp/fukt [3] Meteorologisk vinter/höst");
            var userAnswer = Console.ReadKey(true);
            switch (userAnswer.KeyChar)
            {
                case '1':
                    Sorting.SortValuesBy(avgData, insideOutside);
                    break;
                case '2':
                    Sorting.GetDailyValues(avgData, insideOutside);
                    break;
                case '3':
                    int lowerstTemp = 0;
                    int higestTemp = 0;
                    Console.WriteLine("[1]Vinter [2]Höst ");
                    var userNumber1 = Console.ReadKey(true);

                    if (userNumber1.KeyChar == '1')
                    {
                        lowerstTemp = -273;
                        higestTemp = 0;
                    }
                    else if (userNumber1.KeyChar == '2')
                    {
                        lowerstTemp = 0;
                        higestTemp = 10;
                    }
                    Search.AutumnWinter(avgData, lowerstTemp, higestTemp);
                    break;
            }
        }
    }
}
