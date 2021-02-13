namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class EnumerableExactLengthError : ValidationError
    {
        public EnumerableExactLengthError(string prop_name, int len)
            : base(prop_name,
                  string.Format("Length of '{0}' must be equal {1}",
                      prop_name, len))
        {

        }
    }
}
