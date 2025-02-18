using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MickesVäder
{
    internal class Helpers
    {
        public static int GetUserIntInput(string prompt, params int[] validChoices)
        {
            Console.WriteLine(prompt);

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                try
                {
                    int result = int.Parse(keyInfo.KeyChar.ToString());
                    return validChoices.Contains(result) ? result : throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Ogiltigt val, försök igen...");
                }
            }
        }
    }
}
