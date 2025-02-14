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
            Console.WriteLine("[1] Innomhus [2] Utomhus");
            var userNumber = Console.ReadKey(true);
            string insideOutside = "";
            if(userNumber.KeyChar == '1')
            {
                insideOutside = "Inne";
            }
            else if(userNumber.KeyChar == '2')
            {
                insideOutside = "Ute";
            }
            Collection<WeatherData> dataOutside = ReadWeatherFile.ReadAll("tempdata5-med fel.txt", insideOutside);
            Collection<WeatherData> avgData = Search.CalculateAvg(dataOutside);
            Console.WriteLine("val");
            var userAnswer = Console.ReadKey(true);
            switch (userAnswer.KeyChar)
            {
                case '1':
                    Sorting.SortValuesBy(avgData);
                    break;
                case '2':
                    Sorting.GetDailyValues(avgData);
                    break;
                case '3':
                    Search.AutumnWinter(avgData, true);
                    break;
            }
        }
    }
}
