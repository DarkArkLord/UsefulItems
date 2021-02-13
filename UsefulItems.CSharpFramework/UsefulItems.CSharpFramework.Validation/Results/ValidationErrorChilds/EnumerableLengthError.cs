namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class EnumerableLengthError : ValidationError
    {
        public EnumerableLengthError(string prop_name, int min, int max)
            : base(prop_name, "")
        {
            ErrorMessage = string.Format(
                "Length of '{0}' must be between {1} and {2}",
                prop_name, min, max);
        }
    }
}
