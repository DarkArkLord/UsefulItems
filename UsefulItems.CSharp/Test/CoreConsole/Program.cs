using System;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;
using UsefulItems.CSharp.Trash.Hacks;

namespace UsefulItems.CSharp.Test.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Core!");

            var point = new Vector2(3, 3);
            Console.WriteLine(point);
            point = CentralСoordinateSystemConverter.ConvertToScreen(point, 10, 10);
            Console.WriteLine(point);
            point = CentralСoordinateSystemConverter.ConvertToCentral(point, 10, 10);
            Console.WriteLine(point);

            Console.WriteLine();

            point = new Vector2(3, 3);
            var converter = new CentralСoordinateSystemConverter(10, 10);
            Console.WriteLine(point);
            point = converter.ConvertToScreen(point);
            Console.WriteLine(point);
            point = converter.ConvertToCentral(point);
            Console.WriteLine(point);

            Console.WriteLine("\nend");
            Console.ReadKey();
        }
    }
}
