using UsefulItems.CSharpFramework.Validation.Iternal;
using UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds;
using System;

namespace UsefulItems.CSharpFramework.Validation.PropertyValidatorChilds
{
    public class CompareValidator<TProp> : PropertyValidator
    {
        private Func<TProp, TProp, int> comparer;
        private TProp to_compare;
        private CompareValidationType cvt;
        private CompareValidationStrictType cvst;

        public CompareValidator(string prop_name, TProp to_compare, Func<TProp, TProp, int> comparer, CompareValidationType cvt, CompareValidationStrictType cvst, Func<TProp, string> to_str = null)
        {
            Error = new CompareError<TProp>(prop_name, to_compare, cvt, cvst, to_str);
            AtError = ActionAtError.Continue;
            this.comparer = comparer;
            this.to_compare = to_compare;
            this.cvt = cvt;
            this.cvst = cvst;
        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;

            int cmp = comparer((TProp)value, to_compare);
            if (cvst == CompareValidationStrictType.Mild && cmp == 0)
            {
                return true;
            }

            switch (cvt)
            {
                case CompareValidationType.Less:
                    return cmp < 0;
                case CompareValidationType.Greater:
                    return cmp > 0;
            }

            return false;
        }
    }
}
