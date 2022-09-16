using System.Collections.Generic;
using System.Linq;

namespace UsefulItems.CSharp.ValidationLib.Results
{
    public class ValidationResult
	{
		private readonly List<ValidationError> errors;
		public virtual bool IsValid => Errors.Count == 0;
		public IList<ValidationError> Errors => errors;

		public ValidationResult()
		{
			errors = new List<ValidationError>();
		}

		public ValidationResult(IEnumerable<ValidationError> errors)
		{
			this.errors = errors.Where(x => x != null).ToList();
		}

		public string ToString(string separator)
        {
			return string.Join(separator, errors);
        }
	}
}
