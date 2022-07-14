namespace UsefulItems.CSharp.ValidationLib.Results.ValidationErrorChilds
{
    public class RequiredError : ValidationError
    {
        public RequiredError(string prop_name)
            : base(prop_name, string.Format("'{0}' is required but not initialized", prop_name))
        {

        }
    }
}
