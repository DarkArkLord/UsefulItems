using UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds;
using System;

namespace UsefulItems.CSharpFramework.Validation.PropertyValidatorChilds
{
    public class SpecifiedConditionValidator<TProp> : PropertyValidator
    {
        private Predicate<TProp> predicate;

        public SpecifiedConditionValidator(string param_name, Predicate<TProp> predicate)
        {
            Error = new SpecifiedConditionError(param_name);
            AtError = ActionAtError.Continue;
            this.predicate = predicate;
        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;
            if (predicate is null) return false;
            return predicate((TProp)value);
        }
    }
}
