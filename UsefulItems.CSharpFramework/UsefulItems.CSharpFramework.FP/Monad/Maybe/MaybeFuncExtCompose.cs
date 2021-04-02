using System;

namespace UsefulItems.CSharpFramework.FP.Monad.Maybe
{
    /// <summary>
    /// Композиция фцнкций, работающих с Maybe.
    /// </summary>
    /// <remarks>Набор методов расширения для работы со структурой Maybe.</remarks>
    public static class MaybeFuncExtCompose
    {
        public static Func<T1, Maybe<T3>> Compose<T1, T2, T3>(Func<T1, Maybe<T2>> func1, Func<T2, Maybe<T3>> func2) => value => func1(value).Bind(func2);

        public static Func<T1, Maybe<T3>> Compose<T1, T2, T3>(Func<T1, Maybe<T2>> func1, Func<T2, T3> func2) => value => func1(value).Bind(func2);

        public static Func<T1, Maybe<T3>> Compose<T1, T2, T3>(Func<T1, T2> func1, Func<T2, Maybe<T3>> func2) => value => Maybe<T2>.Unit(func1(value)).Bind(func2);

        public static Func<T1, Maybe<T3>> Compose<T1, T2, T3>(Func<T1, T2> func1, Func<T2, T3> func2) => value => Maybe<T2>.Unit(func1(value)).Bind(func2);
    }
}
