using System;

namespace UsefulItems.CSharp.FunctionalProgramming.Monad.Maybe
{
    /// <summary>
    /// Обертка, позволяющая хранить значение или его отсутствие
    /// </summary>
    /// <typeparam name="T">Тип значения</typeparam>
    public partial struct Maybe<T>
    {
        /// <summary>
        /// Ссылка на значение.
        /// </summary>
        private object _value;

        /// <summary>
        /// Распаковать значение.
        /// </summary>
        public T Value => (T)_value;

        /// <summary>
        /// Проверить на наличие значения.
        /// </summary>
        public bool HasValue => !(_value is null);

        public override string ToString() =>
            HasValue
                ? $"Maybe[{Value}]"
                : "Maybe.Nothing";
    }
}
