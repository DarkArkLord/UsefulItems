using UsefulItems.CSharpFramework.Validation.Results;

namespace UsefulItems.CSharpFramework.Validation.Interfaces
{
    public interface IPropertyValidator
    {
        ActionAtError AtError { get; }
        bool IsValid(object value);
        ValidationError Error { get; }
    }
}
