namespace UsefulItems.CSharpFramework.Validation.Results.ValidationErrorChilds
{
    public class SpecifiedConditionError : ValidationError
    {
        public SpecifiedConditionError(string param_name)
            : base(param_name, 
                  string.Format("The specified condition was not met for '{0}'.",
                      param_name))
        {

        }
    }
}
