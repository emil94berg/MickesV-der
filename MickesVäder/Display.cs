using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class Display
    {
        public static void Menu(Collection<WeatherData> weatherDatas)
        {
            string insideOutside = "";

            Console.Clear();
            Console.WriteLine("[1] Innomhus [2] Utomhus");
            var userNumber = Console.ReadKey(true);
            if(userNumber.KeyChar == '1')
            {
                insideOutside = "Inne";
            }
            else if(userNumber.KeyChar == '2')
            {
                insideOutside = "Ute";
            }

            Console.Clear();
            Console.WriteLine("[1] Sortera meny [2] Dagliga/Månadsvis medel temp/fukt [3] Meteorologisk vinter/höst");
            var userAnswer = Console.ReadKey(true);
            switch (userAnswer.KeyChar)
            {
                case '1':
                    Console.WriteLine("[1] Sortera efter temp [2] Sortera efter fuktighet" + (insideOutside == "Ute" ? "[3] Sortera efter mögelrisk" : ""));
                    var userAnswer2 = Console.ReadKey(true);

                    Console.Clear();
                    switch (userAnswer2.KeyChar)
                    {
                        case '1':
                            DataSet(Calculate.Avg(weatherDatas, true).Where(x => x.Location == insideOutside).OrderByDescending(x => x.Temp));
                            break;
                        case '2':
                            DataSet(Calculate.Avg(weatherDatas, true).Where(x => x.Location == insideOutside).OrderBy(x => x.Moist));
                            break;
                        case '3':
                            DataSet(Calculate.Avg(weatherDatas, true).Where(x => x.Location == insideOutside).OrderBy(x => x.Mold));
                            break;
                    }

                    break;
                case '2':
                    Console.WriteLine("Vilket datum vill du kolla närmare på?");
                    string selectedDate = Console.ReadLine();

                    GetValues.Daily(Calculate.Avg(weatherDatas, true), insideOutside, selectedDate);
                    break;
                case '3':
                    int lowerstTemp = 0;
                    int higestTemp = 0;

                    Console.Clear();
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

                    Console.Clear();
                    Console.WriteLine(Calculate.AutumnOrWinter(Calculate.Avg(weatherDatas, true), lowerstTemp, higestTemp));
                    break;
            }
            Console.ReadKey(true);
        }

        public static void DataSet(IEnumerable<WeatherData> weatherData)
        {
            foreach (var entry in weatherData)
            {
                Console.WriteLine($"Datum: {entry.Date}\t Temp: {entry.Temp}  \t Fukt: {entry.Moist}%\t Mögelrisk: {entry.Mold}%\t Plats: {entry.Location}"); 
            }
        }
    }
}
