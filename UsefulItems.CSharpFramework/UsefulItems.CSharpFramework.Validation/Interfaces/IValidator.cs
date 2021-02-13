using UsefulItems.CSharpFramework.Validation.Results;
using System.Collections.Generic;

namespace UsefulItems.CSharpFramework.Validation.Interfaces
{
    public interface IValidator<T> 
    {
        List<IValidationRule<T>> Rules { get; }
        ValidationResult Validate(T instance);
    }
}
