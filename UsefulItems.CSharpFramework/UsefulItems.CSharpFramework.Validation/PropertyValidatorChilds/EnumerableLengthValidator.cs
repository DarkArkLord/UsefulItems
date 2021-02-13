using UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds;
using System.Collections.Generic;
using System.Linq;

namespace UsefulItems.CSharpFramework.Validation.PropertyValidatorChilds
{
    public class EnumerableLengthValidator<TProp> : PropertyValidator
    {
        private int min, max;
        public EnumerableLengthValidator(string prop_name, int min, int max)
        {
            Error = new EnumerableLengthError(prop_name, min, max);
            AtError = ActionAtError.Continue;
            this.min = min;
            this.max = max;
        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;

            int l = ((IEnumerable<TProp>)value).Count();
            return min <= l && l <= max;
        }
    }
}
