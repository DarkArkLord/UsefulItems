using UsefulItems.CSharpFramework.Validation.Iternal;
using System;

namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class CompareError<TProp> : ValidationError
    {
        public CompareError(string prop_name, TProp value, CompareValidationType cvt, CompareValidationStrictType cvst, Func<TProp, string> to_str = null)
            : base(prop_name, "")
        {
            string cv = "";
            switch (cvt)
            {
                case CompareValidationType.Less:
                    cv = "less";
                    break;
                case CompareValidationType.Greater:
                    cv = "greater";
                    break;
            }

            string cvs = "";
            switch (cvst)
            {
                case CompareValidationStrictType.Mild:
                    cvs = " or equal";
                    break;
                case CompareValidationStrictType.Strict:
                    cvs = "";
                    break;
            }

            ErrorMessage = string
                .Format(
                    "'{0}' must be {1}{2} then '{3}'",
                    prop_name,
                    cv, cvs,
                    to_str is null ? value.ToString() : to_str(value));
        }
    }
}
