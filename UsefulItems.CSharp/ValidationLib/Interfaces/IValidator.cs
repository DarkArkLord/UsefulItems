using System.Collections.Generic;
using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib.Interfaces
{
    public interface IValidator<T> 
    {
        List<IValidationRule<T>> Rules { get; }
        ValidationResult Validate(T instance);
    }
}
