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

                    if (validChoices.Contains(result))
                        return result;
                    else
                    {
                        throw new ArgumentOutOfRangeException($"{result} är ett ogiltigt menyval.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"FormatException: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"ArgumentOutOfRangeException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }
}
