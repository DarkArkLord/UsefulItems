using System;

namespace UsefulItems.CSharpFramework.FP.Monad.Maybe
{
    /// <summary>
    /// Получение значения из Maybe.
    /// </summary>
    /// <remarks>Набор методов расширения для работы со структурой Maybe.</remarks>
    public static class MaybeExtGet
    {
        public static T Pure<T>(this Maybe<T> maybe)
        {
            if (!maybe.HasValue)
            {
                throw new NullReferenceException();
            }

            return maybe.Value;
        }

        public static T PureOr<T>(this Maybe<T> maybe, T else_value) =>
            maybe.HasValue
                ? maybe.Value
                : else_value;

        public static T PureOrGet<T>(this Maybe<T> maybe, Func<T> else_func) =>
            maybe.HasValue
                ? maybe.Value
                : else_func();

        public static T PureOrThrow<T>(this Maybe<T> maybe, Exception else_exception) => 
            maybe.HasValue 
                ? maybe.Value 
                : throw else_exception;
    }
}
