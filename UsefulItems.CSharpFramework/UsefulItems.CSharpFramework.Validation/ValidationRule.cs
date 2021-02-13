using UsefulItems.CSharpFramework.Validation.Interfaces;
using UsefulItems.CSharpFramework.Validation.Iternal;
using UsefulItems.CSharpFramework.Validation.Results;
using System.Collections.Generic;

namespace UsefulItems.CSharpFramework.Validation
{
    public class ValidationRule<T> : IValidationRule<T>
    {
        private IProperty<T> property;
        public List<IPropertyValidator> Validators { get; protected set; }

        public ValidationRule(IProperty<T> property)
        {
            property.CheckNull(nameof(property));
            this.property = property;
            Validators = new List<IPropertyValidator>();
        }

        public void AddValidator(IPropertyValidator validator)
        {
            validator.CheckNull(nameof(validator));
            Validators.Add(validator);
        }

        public IEnumerable<ValidationError> Validate(T instance)
        {
            instance.CheckNull(nameof(instance));
            object value = property.Value(instance);

            foreach(var validator in Validators)
            {
                if (validator.IsValid(value)) continue;
                if (validator.AtError == ActionAtError.Stop) break;
                yield return validator.Error;
                if (validator.AtError == ActionAtError.Break) break;
            }
        }

        public IProperty<T> Property => property;
    }
}
