using System;
using UsefulItems.CSharp.ValidationLib.Interfaces;
using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib
{
    public class RuleBuilder<T, TProp> : IRuleBuilder<T, TProp>
    {
        public IValidationRule<T> Rule { get; private set; }

        public RuleBuilder(IValidationRule<T> rule)
        {
            Rule = rule;
        }

        public void AddValidator(Predicate<TProp> predicate, ValidationError error, ActionAtError actionAtError)
        {
            Predicate<object> o_pred = (x) => predicate((TProp)x);
            PropertyValidator validator = new PropertyValidator(o_pred, error, actionAtError);
            Rule.AddValidator(validator);
        }

        public void AddValidator(IPropertyValidator validator)
        {
            Rule.AddValidator(validator);
        }
    }
}
