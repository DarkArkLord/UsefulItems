namespace UsefulItems.CSharpFramework.Validation.Results
{
    public class ValidationError
	{
		public string PropertyName { get; set; }
		public string ErrorMessage { get; set; }

		public ValidationError(string property_name, string error_message)
		{
			PropertyName = property_name;
			ErrorMessage = error_message;
		}

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
