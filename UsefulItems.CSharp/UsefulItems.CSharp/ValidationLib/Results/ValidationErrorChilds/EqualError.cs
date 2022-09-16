using System;

namespace UsefulItems.CSharp.ValidationLib.Results.ValidationErrorChilds
{
    public class EqualError<TProp> : ValidationError
    {
        public EqualError(string prop_name, TProp value, Func<TProp, string> to_str = null)
            : base(prop_name, "")
        {
            ErrorMessage = string
                .Format(
                    "'{0}' must be equal '{1}'",
                    prop_name,
                    to_str is null ? value.ToString() : to_str(value));
        }
    }
}
