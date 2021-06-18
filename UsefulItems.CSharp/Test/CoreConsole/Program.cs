using System;
using UsefulItems.CSharp.FunctionalProgramming.Monad.Maybe;

namespace UsefulItems.CSharp.Test.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Core!");

            var tests = new (Maybe<int> a, Maybe<int> b)[]
            {
                (Maybe<int>.Just(1), Maybe<int>.Just(1)),
                (Maybe<int>.Just(1), Maybe<int>.Just(2)),
                (Maybe<int>.Just(1), Maybe<int>.Nothing),
                (Maybe<int>.Just(2), Maybe<int>.Just(1)),
                (Maybe<int>.Just(2), Maybe<int>.Just(2)),
                (Maybe<int>.Just(2), Maybe<int>.Nothing),
                (Maybe<int>.Nothing, Maybe<int>.Just(1)),
                (Maybe<int>.Nothing, Maybe<int>.Just(2)),
                (Maybe<int>.Nothing, Maybe<int>.Nothing),
            };

            foreach (var test in tests)
            {
                Check(test.a, test.b);
            }

            Console.WriteLine("\nend");
            Console.ReadKey();
        }

        static void Check(Maybe<int> a, Maybe<int> b)
        {
            Console.WriteLine($"({a}, {b}) -> {a == b}");
        }
    }
}
