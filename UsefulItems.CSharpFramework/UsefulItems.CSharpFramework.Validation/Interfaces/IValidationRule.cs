using UsefulItems.CSharpFramework.Validation.Results;
using System.Collections.Generic;

namespace UsefulItems.CSharpFramework.Validation.Interfaces
{
    public interface IValidationRule<T>
    {
        List<IPropertyValidator> Validators { get; }
        void AddValidator(IPropertyValidator validator);
        IEnumerable<ValidationError> Validate(T instance);
        IProperty<T> Property { get; }
    }
}
