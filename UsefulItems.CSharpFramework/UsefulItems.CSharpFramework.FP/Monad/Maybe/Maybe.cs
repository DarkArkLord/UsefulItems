namespace UsefulItems.CSharpFramework.FP.Monad.Maybe
{
    public struct Maybe<T>
    {
        /// <summary>
        /// Ссылка на значение.
        /// </summary>
        private object value;

        /// <summary>
        /// Распаковать знаечние.
        /// </summary>
        public T Value => (T)value;

        /// <summary>
        /// Проверить на наличие значения.
        /// </summary>
        public bool HasValue => !(value is null);

        #region Init
        /// <summary>
        /// Инициализация по значению.
        /// </summary>
        /// <param name="value">Начальное значение</param>
        /// <returns>Экземпляр Maybe с переданным значением внутри.</returns>
        public static Maybe<T> Unit(T value)
        {
            Maybe<T> maybe = new Maybe<T> { value = value };
            return maybe;
        }

        /// <summary>
        /// Пустой объект.
        /// </summary>
        public static Maybe<T> Empty { get; } = new Maybe<T> { value = null }; 
        #endregion

        public override string ToString() =>
            HasValue
                ? $"Maybe[{Value}]"
                : "Maybe.Empty";
    }
}
