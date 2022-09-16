using System;

namespace UsefulItems.CSharp.FunctionalProgramming.Monad.Maybe
{
    /// <summary>
    /// Обертка, позволяющая хранить значение или его отсутствие
    /// </summary>
    /// <typeparam name="T">Тип значения</typeparam>
    public partial struct Maybe<T> : IEquatable<Maybe<T>>, IEquatable<T>
    {
        public bool Equals(Maybe<T> other) => this.IsEquals(other);

        public bool Equals(T other) => HasValue && this.IsEquals(Just(other));

        public override bool Equals(object obj)
        {
            return (obj is Maybe<T> otherMaybe && Equals(otherMaybe))
                || (obj is T otherT && Equals(otherT));
        }

        public override int GetHashCode()
        {
            return HasValue ? Value.GetHashCode() : 0;
        }

        public static bool operator ==(Maybe<T> a, Maybe<T> b) => a.Equals(b);

        public static bool operator !=(Maybe<T> a, Maybe<T> b) => !a.Equals(b);

        public static bool operator ==(Maybe<T> a, T b) => a.Equals(b);

        public static bool operator !=(Maybe<T> a, T b) => !a.Equals(b);
    }
}
