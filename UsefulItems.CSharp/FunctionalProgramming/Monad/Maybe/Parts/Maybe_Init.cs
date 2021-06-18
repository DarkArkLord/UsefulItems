using System;

namespace UsefulItems.CSharp.FunctionalProgramming.Monad.Maybe
{
    public partial struct Maybe<T> 
    {
        /// <summary>
        /// Инициализация по значению.
        /// </summary>
        /// <param name="value">Начальное значение</param>
        /// <returns>Экземпляр Maybe с переданным значением внутри.</returns>
        public static Maybe<T> Just(T value)
        {
            var maybe = new Maybe<T> { _value = value };
            return maybe;
        }

        /// <summary>
        /// Пустой объект.
        /// </summary>
        public static Maybe<T> Nothing { get; } = new Maybe<T> { _value = null };
    }
}
