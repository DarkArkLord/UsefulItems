using UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds;
using System;

namespace UsefulItems.CSharpFramework.Validation.PropertyValidatorChilds
{
    public class EqualValidator<TProp> : PropertyValidator
    {
        private Func<TProp, TProp, bool> comparer;
        private TProp to_compare;

        public EqualValidator(string prop_name, TProp to_compare, Func<TProp, TProp, bool> comparer = null, Func<TProp, string> to_str = null)
        {
            Error = new EqualError<TProp>(prop_name, to_compare, to_str);
            AtError = ActionAtError.Continue;
            this.comparer = comparer;
            this.to_compare = to_compare;
        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;

            if (comparer is null)
            {
                return value.Equals(to_compare);
            }

            return comparer((TProp)value, to_compare);
        }
    }
}
