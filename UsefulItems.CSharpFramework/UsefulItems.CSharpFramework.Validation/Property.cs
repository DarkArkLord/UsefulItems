using UsefulItems.CSharpFramework.Validation.Interfaces;
using UsefulItems.CSharpFramework.Validation.Iternal;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace UsefulItems.CSharpFramework.Validation
{
    public class Property<T> : IProperty<T>
    {
        private Func<T, object> value_func;

        public Property(Func<T, object> expression, string name)
        {
            expression.CheckNull(nameof(expression));
            name.CheckNull(nameof(name));
            value_func = expression;
            Name = name;
        }

        protected Property() { }

        public object Value(T instance) => value_func(instance);

        public string Name { get; private set; }

        public static Property<T> Create<TProp>(Expression<Func<T, TProp>> expression)
        {
            Property<T> property = new Property<T>();

            property.value_func = x => expression.Compile()(x);

            MemberInfo member = expression.GetMember();

            property.Name = member.Name;

            return property;
        }
    }
}
