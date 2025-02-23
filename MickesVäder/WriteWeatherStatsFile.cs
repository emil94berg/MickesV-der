﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    
    internal static class WriteWeatherStatsFile
    {
        public static string path = "../../../Files/";
        public static void SaveToFile(this List<string> text, string fileName)
        {
            using (StreamWriter streamwrite = new StreamWriter(path + fileName, false))
            {
                foreach (string line in text) streamwrite.WriteLine(line);
            }
        }
        public static void SaveMonthlyAverages(Collection<WeatherData> weatherData)
        {
            Collection<WeatherData> montlyData = Calculate.Avg(weatherData, false);
            List<string> allDataTxt = new List<string>();

            foreach (WeatherData data in montlyData)
            {
                allDataTxt.Add($"Datum: {data.Date.Year}-{data.Date.Month}\t Temp: {data.Temp}  \t Fukt: {data.Moist}\t Mögelrisk: {data.Mold}\t Plats: {data.Location}");
            }

            allDataTxt.Add("Höst: " + Calculate.AutumnOrWinter(Calculate.Avg(weatherData, true), 0, 10));
            allDataTxt.Add("Vinter: " + Calculate.AutumnOrWinter(Calculate.Avg(weatherData, true), -273, 0));
            allDataTxt.Add("((days.Average(x => x.Moist)) >= 78 && (days.Average(x => x.Temp) > 0)) ? Math.Round(((days.Average(x => x.Moist) - 78) * (days.Average(x => x.Temp) / 15) / 0.22), 0) : 0");
            allDataTxt.SaveToFile("WeatherStats.txt");
        } 

        
    }
}
