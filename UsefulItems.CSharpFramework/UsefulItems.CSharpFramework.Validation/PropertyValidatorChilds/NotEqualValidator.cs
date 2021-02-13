using UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds;
using System;

namespace UsefulItems.CSharpFramework.Validation.PropertyValidatorChilds
{
    public class NotEqualValidator<TProp> : PropertyValidator
    {
        private Func<TProp, TProp, bool> comparer;
        private TProp to_compare;

        public NotEqualValidator(string prop_name, TProp to_compare, Func<TProp, TProp, bool> comparer = null, Func<TProp, string> to_str = null)
        {
            Error = new NotEqualError<TProp>(prop_name, to_compare, to_str);
            AtError = ActionAtError.Continue;
            this.comparer = comparer;
            this.to_compare = to_compare;
        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;

            bool res = false;

            if (comparer is null)
            {
                res = value.Equals(to_compare);
            }
            else
            {
                res = comparer((TProp)value, to_compare);
            }

            return !res;
        }
    }
}
