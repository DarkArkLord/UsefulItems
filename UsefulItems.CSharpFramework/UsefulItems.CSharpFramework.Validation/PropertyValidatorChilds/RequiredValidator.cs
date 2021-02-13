using UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds;

namespace UsefulItems.CSharpFramework.Validation.PropertyValidatorChilds
{
    public class RequiredValidator<TProp> : PropertyValidator
    {
        public RequiredValidator(string prop_name)
            : base()
        {
            Error = new RequiredError(prop_name);
            AtError = ActionAtError.Break;
        }

        public override bool IsValid(object value)
        {
            if (value is null) return false;

            return !((TProp)value).Equals(default(TProp));
        }
    }
}
