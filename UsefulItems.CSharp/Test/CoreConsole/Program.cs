using System;
using UsefulItems.CSharp.Trash.Hacks;

namespace UsefulItems.CSharp.Test.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Core!");

            StringHack.Test();

            Console.WriteLine("\nend");
            Console.ReadKey();
        }
    }
}
