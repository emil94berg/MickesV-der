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
            int userNumber = Helpers.GetUserIntInput("[1] Innomhus [2] Utomhus [0] Avsluta programmet", 1, 2, 0);
            if (userNumber == 1)
            {
                insideOutside = "Inne";
            }
            else if(userNumber == 2)
            {
                insideOutside = "Ute";
            }
            else if(userNumber == 0)
            {
                Environment.Exit(0);
            }

            Console.Clear();
            int userAnswer = Helpers.GetUserIntInput("[1] Sortera meny [2] Dagliga/Månadsvis medel temp/fukt [3] Meteorologisk vinter/höst", 1, 2, 3);
            switch (userAnswer)
            {
                case 1:
                    int userAnswer2 = Helpers.GetUserIntInput("[1] Sortera efter temp [2] Sortera efter fuktighet" + (insideOutside == "Ute" ? " [3] Sortera efter mögelrisk" : ""),
                        insideOutside == "Ute" ? new int[] { 1, 2, 3 } : new int[] { 1, 2 }
                        );

                    Console.Clear();
                    switch (userAnswer2)
                    {
                        case 1:
                            DataSet(Calculate.Avg(weatherDatas, true).Where(x => x.Location == insideOutside).OrderByDescending(x => x.Temp));
                            break;
                        case 2:
                            DataSet(Calculate.Avg(weatherDatas, true).Where(x => x.Location == insideOutside).OrderBy(x => x.Moist));
                            break;
                        case 3:
                            DataSet(Calculate.Avg(weatherDatas, true).Where(x => x.Location == insideOutside).OrderBy(x => x.Mold));
                            break;
                    }

                    break;
                case 2:
                    Console.WriteLine("Vilket datum vill du kolla närmare på?");
                    string selectedDate = Console.ReadLine();

                    GetValues.Daily(Calculate.Avg(weatherDatas, true), insideOutside, selectedDate);
                    break;
                case 3:
                    int lowerstTemp = 0;
                    int higestTemp = 0;

                    Console.Clear();
                    int userNumber1 = Helpers.GetUserIntInput("[1]Vinter [2]Höst", 1, 2);
                    if (userNumber1 == 1)
                    {
                        lowerstTemp = -273;
                        higestTemp = 0;
                    }
                    else if (userNumber1 == 2)
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
