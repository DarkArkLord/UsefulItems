using System.Collections.Generic;
using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib.Interfaces
{
    public interface IValidationRule<T>
    {
        List<IPropertyValidator> Validators { get; }
        void AddValidator(IPropertyValidator validator);
        IEnumerable<ValidationError> Validate(T instance);
        IProperty<T> Property { get; }
    }
}
