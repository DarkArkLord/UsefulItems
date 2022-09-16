using System.Collections.Generic;
using UsefulItems.CSharp.ValidationLib.Interfaces;
using UsefulItems.CSharp.ValidationLib.Iternal;
using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib
{
    public class ValidationEnumerableRule<T, TPropElem> : IValidationRule<T>
    {
        private IProperty<T> property;
        public List<IPropertyValidator> Validators { get; private set; }

        public ValidationEnumerableRule(IProperty<T> property)
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
            var sequence = property.Value(instance);
            if(sequence is null)
            {
                yield break;
            }

            foreach (TPropElem value in sequence as IEnumerable<TPropElem>)
            {
                foreach (var validator in Validators)
                {
                    if (validator.IsValid(value)) continue;
                    if (validator.AtError == ActionAtError.Stop) break;
                    yield return validator.Error;
                    if (validator.AtError == ActionAtError.Break) break;
                }
            }
        }

        public IProperty<T> Property => property;
    }
}
