using UsefulItems.CSharpFramework.Validation.Interfaces;
using UsefulItems.CSharpFramework.Validation.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UsefulItems.CSharpFramework.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        public List<IValidationRule<T>> Rules { get; protected set; }

        public AbstractValidator()
        {
            Rules = new List<IValidationRule<T>>();
        }

        public virtual ValidationResult Validate(T instance)
        {
            ValidationResult res = new ValidationResult();

            foreach(var rule in Rules)
            {
                var errors = rule.Validate(instance);

                foreach(var error in errors)
                {
                    res.Errors.Add(error);
                }
            }

            return res;
        }

        public IRuleBuilder<T, TProp> CreateRule<TProp>(IProperty<T> property)
        {
            ValidationRule<T> rule = new ValidationRule<T>(property);
            Rules.Add(rule);
            RuleBuilder<T, TProp> builder = new RuleBuilder<T, TProp>(rule);
            return builder;
        }

        public IRuleBuilder<T, TProp> CreateRule<TProp>(Expression<Func<T, TProp>> func)
        {
            Property<T> property = Property<T>.Create(func);
            return CreateRule<TProp>(property);
        }

        public IRuleBuilder<T, TPropElem> CreateRuleForEach<TPropElem>(IProperty<T> property)
        {
            ValidationEnumerableRule<T, TPropElem> rule = new ValidationEnumerableRule<T, TPropElem>(property);
            Rules.Add(rule);
            RuleBuilder<T, TPropElem> builder = new RuleBuilder<T, TPropElem>(rule);
            return builder;
        }

        public IRuleBuilder<T, TPropElem> CreateRuleForEach<TPropElem>(Expression<Func<T, IEnumerable<TPropElem>>> func)
        {
            Property<T> property = Property<T>.Create(func);
            return CreateRuleForEach<TPropElem>(property);
        }
    }
}
