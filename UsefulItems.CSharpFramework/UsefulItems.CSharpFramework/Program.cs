using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsefulItems.CSharpFramework.FP.Monad.Maybe;

namespace UsefulItems.CSharpFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");

            Maybe<string> maybe = Maybe<string>.Unit(null);
            Console.WriteLine(maybe); 
            maybe = Maybe<string>.Unit("123");
            Console.WriteLine(maybe);

            Console.WriteLine("\nend");
            Console.ReadKey();
        }
    }
}
