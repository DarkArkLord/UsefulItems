namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class NotNullError : ValidationError
    {
        public NotNullError(string prop_name):
            base(prop_name, string.Format("'{0}' must be not null.", prop_name))
        {

        }
    }
}
