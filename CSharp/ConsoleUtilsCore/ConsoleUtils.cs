using System;
using System.Linq;

namespace ConsoleUtilsCore
{
    public static class ConsoleUtils
    {
        public static string ReadStringFromVariants(params string[] valiants)
        {
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.Trim();
                if (input is not null && valiants.Any(x => x == input))
                {
                    return input;
                }
                Console.WriteLine("> Некорректный ввод, повторите попытку.");
            }
        }
    }
}
