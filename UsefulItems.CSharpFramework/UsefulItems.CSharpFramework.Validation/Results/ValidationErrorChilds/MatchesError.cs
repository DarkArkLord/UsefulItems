namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class MatchesError : ValidationError
    {
        public MatchesError(string param_name)
            :base (param_name, 
                 string.Format("'{0}' is not in the correct format.", param_name))
        {

        }
    }
}
