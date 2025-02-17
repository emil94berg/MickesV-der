using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class Sorting
    {
        //public static List<string> GetAllOnLocation (string[] dataSet, string location)
        //{
        //    List<string> result = new List<string>();
        //    //grupp 5 inne/ute
        //    Regex regex = new Regex("^(?<year>\\d{4})-(?<month>\\d{2})-(?<day>\\d{2})\\W(?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>(\\b|\\-)\\d{1,2}\\.\\d),(?<moist>\\d{1,3})$");
            
        //    foreach (var line in dataSet)
        //    {
        //        Match match = regex.Match(line);
        //        if (match.Success && (match.Groups["location"].Value == location))
        //        {
        //            result.Add(line);
                    
        //        }
        //    }
        //    return result;  
        //}
        public static void GetDailyValues (Collection<WeatherData> data, string insideOutside)
        {
            
            Console.WriteLine("Vilket datum vill du kolla närmare på?");
            string userAnswer = Console.ReadLine();
            Regex regex = new Regex("(?<year>\\d{4}).(?<month>0[1-9]|1[0-2]).(?<day>0[1-9]|[1-2][0-9]|3[0-1])");
            Match match = regex.Match(userAnswer);
            Regex regex1 = new Regex("^(?<year>\\d{4}).(?<month>0[1-9]|1[0-2])$");
            Match match1 = regex1.Match(userAnswer);
            if (match1.Success)
            {
                var test = (from x in data
                            where x.Date.ToString().Contains(match1.Groups["year"].Value + "-" + match1.Groups["month"].Value) && x.Location == insideOutside
                            select x).ToList();
                foreach (var x in test)
                {
                    Console.WriteLine($"Dag: {x.Date.ToString()} temp: {x.Temp} fukt: {x.Moist} plats: {x.Location}");
                }
            }
            else if (match.Success)
            {
                DateOnly checkDate = new DateOnly(int.Parse(match.Groups["year"].Value), int.Parse(match.Groups["month"].Value), int.Parse(match.Groups["day"].Value));
                if (data.Any(x => x.Date.Equals(checkDate)))
                {
                    foreach (var avgWeather in data.Where(x => x.Date == checkDate && x.Location == insideOutside))
                    {
                        Console.WriteLine($"Datum: {avgWeather.Date} temperatur: {avgWeather.Temp} fuktighet: {avgWeather.Moist} plats: {avgWeather.Location}");
                    } 
                }
            }
            else
            {
                Console.WriteLine("Felaktigt datum");
            }
        }
        public static Collection<WeatherData> GetMonthlyValues(Collection<WeatherData> data)
        {

            Collection < WeatherData > monthlyValues = new Collection<WeatherData >();
            foreach (var weather in data)
            {

            }
            return monthlyValues;
        }



        public static void SortValuesBy(Collection<WeatherData> data, string location)
        {
            
            Console.WriteLine("[1] Sortera efter temp [2] Sortera efter fuktighet [3] Sortera efter mögelrisk");
            var userAnswer = Console.ReadKey(true);
            switch (userAnswer.KeyChar)
            {
                case '1':
                    foreach(var x in data.Where(x => x.Location == location).OrderByDescending(x => x.Temp))
                    {
                        
                        Console.WriteLine($"Dag: {x.Date.ToString()} temp: {x.Temp}");
                    }
                    break;
                case '2':
                    foreach(var x in data.Where(x => x.Location == location).OrderBy(x => x.Moist))
                    {
                        Console.WriteLine($"Dag: {x.Date.ToString()} fukt: {x.Moist}");
                    }
                    break;
                case '3':
                    var test = (from x in data
                                where x.Location == location
                                group x by x.Date into days
                                select new
                                {
                                    AvgDate = days.Key,
                                    Mold = ((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp) / 15) / 0.22), 0) : 0

                                });
                    foreach (var x in test.OrderBy(x => x.Mold))
                    {
                        Console.WriteLine($"Dag: {x.AvgDate} risk för mögel: {x.Mold}");
                    }

                    //foreach (var x in data.Select(x => new { Mold = ((data.Average(x => x.Moist)) >= 78 && (data.Average(x => x.Temp) > 0)) ? Math.Round(((data.Average(x => x.Moist) - 78) * (data.Average(x => x.Temp) / 15) / 0.22), 0) : 0, Date = x.Date }).OrderBy(x => x.Mold))
                    //{
                    //    Console.WriteLine($"Dag: {x.Date} risk för mögel: {x.Mold}");
                    //}
                    break;
            }

        }
    }
}
