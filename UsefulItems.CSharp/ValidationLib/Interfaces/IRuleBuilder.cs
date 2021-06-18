using System;
using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib.Interfaces
{
    public interface IRuleBuilder<T, TProp>
    {
        IValidationRule<T> Rule { get; }
        void AddValidator(Predicate<TProp> predicate, ValidationError error, ActionAtError actionAtError);
        void AddValidator(IPropertyValidator validator);
    }
}
