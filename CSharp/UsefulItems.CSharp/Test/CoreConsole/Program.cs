using System;
using UsefulItems.CSharp.DarkGraphicsLib.CommonElements;
using UsefulItems.CSharp.Trash.Hacks;

namespace UsefulItems.CSharp.Test.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Core!");

            var point = new Vector3(10, 10, 10);
            Console.WriteLine(point);
            Console.WriteLine();

            Console.WriteLine(point.Rotate(new Vector3(0, 0, 0)));
            Console.WriteLine();

            Console.WriteLine(point.Rotate(new Vector3(Math.PI, 0, 0)));
            Console.WriteLine(point.Rotate(new Vector3(Math.PI, 0, 0)));
            Console.WriteLine();

            Console.WriteLine(point.Rotate(new Vector3(0, Math.PI, 0)));
            Console.WriteLine(point.Rotate(new Vector3(0, Math.PI, 0)));
            Console.WriteLine();

            Console.WriteLine(point.Rotate(new Vector3(0, 0, Math.PI)));
            Console.WriteLine(point.Rotate(new Vector3(0, 0, Math.PI)));
            Console.WriteLine();

            Console.WriteLine(point);

            Console.WriteLine("\nend");
            Console.ReadKey();
        }
    }
}
