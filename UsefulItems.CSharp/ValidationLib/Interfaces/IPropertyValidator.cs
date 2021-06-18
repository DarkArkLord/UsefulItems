using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib.Interfaces
{
    public interface IPropertyValidator
    {
        ActionAtError AtError { get; }
        bool IsValid(object value);
        ValidationError Error { get; }
    }
}
