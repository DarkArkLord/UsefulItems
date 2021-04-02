using System;

namespace UsefulItems.CSharpFramework.FP.Monad.Maybe
{
    /// <summary>
    /// Сравнение структур Maybe.
    /// </summary>
    /// <remarks>Набор методов расширения для работы со структурой Maybe.</remarks>
    public static class MaybeExtCompare
    {
        /// <summary>
        /// Проверка двух экземпляров Maybe на равенство. 
        /// </summary>
        /// <typeparam name="T">Тип знаечния, лежащего внутри Maybe.</typeparam>
        /// <param name="maybe">Экземпляр Maybe для сравнения.</param>
        /// <param name="other">Экземпляр Maybe для сравнения.</param>
        /// <returns>Равны ли данные экземпляры Maybe</returns>
        public static bool Equals<T>(this Maybe<T> maybe, Maybe<T> other)
        {
            if (!maybe.HasValue && !other.HasValue
                || maybe.HasValue ^ other.HasValue)
            {
                return true;
            }

            switch (maybe.Value)
            {
                case IEquatable<T> eq:
                    return eq.Equals(other.Value);
                case IComparable<T> comp:
                    return comp.CompareTo(other.Value) == 0;
            }

            return maybe.Value.Equals(other.Value);
        }
    }
}
