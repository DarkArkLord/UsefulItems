using System;

namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class NotEqualError<TProp> : ValidationError
    {
        public NotEqualError(string prop_name, TProp value, Func<TProp, string> to_str = null)
            : base(prop_name, "")
        {
            ErrorMessage = string
                .Format(
                    "'{0}' must be not equal '{1}'",
                    prop_name, 
                    to_str is null ? value.ToString() : to_str(value));
        }
    }
}
