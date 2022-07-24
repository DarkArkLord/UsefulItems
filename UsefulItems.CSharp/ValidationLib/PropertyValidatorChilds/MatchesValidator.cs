using System.Text.RegularExpressions;
using UsefulItems.CSharp.ValidationLib.Results.ValidationErrorChilds;

namespace UsefulItems.CSharp.ValidationLib.PropertyValidatorChilds
{
    public class MatchesValidator : PropertyValidator
    {
        private Regex regex;

        public MatchesValidator(string param_name, Regex regex)
        {
            this.regex = regex;

            Error = new MatchesError(param_name);
            AtError = ActionAtError.Continue;
        }

        public MatchesValidator(string param_name, string expression, RegexOptions options = RegexOptions.None)
            : this(param_name, new Regex(expression, options))
        {

        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;
            if (regex is null) return false;
            return regex.IsMatch((string)value);
        }
    }
}
