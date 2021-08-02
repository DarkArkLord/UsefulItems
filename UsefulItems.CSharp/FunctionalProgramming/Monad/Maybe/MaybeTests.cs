using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulItems.CSharp.FunctionalProgramming.Monad.Maybe
{
    public static class MaybeTests
    {
        public static void Main()
        {
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
        }

        static void Check(Maybe<int> a, Maybe<int> b)
        {
            Console.WriteLine($"({a}, {b}) -> {a == b}");
        }
    }
}
