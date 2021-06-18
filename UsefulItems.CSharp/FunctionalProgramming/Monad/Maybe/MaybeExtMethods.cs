using System;

namespace UsefulItems.CSharp.FunctionalProgramming.Monad.Maybe
{
    /// <summary>
    /// Методы для работы с Maybe.
    /// </summary>
    /// <remarks>Набор методов расширения для работы со структурой Maybe.</remarks>
    public static class MaybeExtMethods
    {
        public static Maybe<T2> Bind<T1, T2>(this Maybe<T1> maybe, Func<T1, Maybe<T2>> func) =>
            maybe.HasValue
                ? func(maybe.Value)
                : Maybe<T2>.Nothing;

        public static Maybe<T2> Bind<T1, T2>(this Maybe<T1> maybe, Func<T1, T2> func) =>
            maybe.Bind(value => Maybe<T2>.Just(func(value)));

        public static Maybe<T> Has<T>(this Maybe<T> maybe, Func<T, bool> predicate) =>
            maybe.HasValue && predicate(maybe.Value) 
                ? maybe 
                : Maybe<T>.Nothing;

        public static Maybe<T> IfHas<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasValue)
            {
                action(maybe.Value);
            }
            return maybe;
        }

        public static Maybe<T> IfNotHas<T>(this Maybe<T> maybe, Action action)
        {
            if (!maybe.HasValue)
            {
                action();
            }
            return maybe;
        }
    }
}
