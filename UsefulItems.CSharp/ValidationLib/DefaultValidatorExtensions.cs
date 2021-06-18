using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UsefulItems.CSharp.ValidationLib.Interfaces;
using UsefulItems.CSharp.ValidationLib.Iternal;
using UsefulItems.CSharp.ValidationLib.PropertyValidatorChilds;
using UsefulItems.CSharp.ValidationLib.Results;

namespace UsefulItems.CSharp.ValidationLib
{
    public static class DefaultValidatorExtensions
    {
        #region Required
        public static IRuleBuilder<T, TProp> Required<T, TProp>(this IRuleBuilder<T, TProp> builder)
        {
            builder.CheckNull(nameof(builder));

            builder.AddValidator(new RequiredValidator<TProp>(builder.Rule.Property.Name));

            return builder;
        }
        #endregion

        #region Null
        public static IRuleBuilder<T, TProp> NotNull<T, TProp>(this IRuleBuilder<T, TProp> builder) where TProp : class
        {
            builder.CheckNull(nameof(builder));

            builder.AddValidator(new NotNullValidator(builder.Rule.Property.Name));

            return builder;
        }
        #endregion

        #region Equal
        public static IRuleBuilder<T, TProp> Equal<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, TProp, bool> comparer = null, Func<TProp, string> to_str = null)
        {
            builder.CheckNull(nameof(builder));
            to_compare.CheckNull(nameof(to_compare));

            builder.AddValidator(new EqualValidator<TProp>(
                builder.Rule.Property.Name,
                to_compare, comparer, to_str));

            return builder;
        }

        public static IRuleBuilder<T, TProp> NotEqual<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, TProp, bool> comparer = null, Func<TProp, string> to_str = null)
        {
            builder.CheckNull(nameof(builder));
            to_compare.CheckNull(nameof(to_compare));

            builder.AddValidator(new NotEqualValidator<TProp>(
                builder.Rule.Property.Name,
                to_compare, comparer, to_str));

            return builder;
        }
        #endregion

        #region Compare
        public static IRuleBuilder<T, TProp> LessThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, TProp, int> comparer, Func<TProp, string> to_str = null)
        {
            builder.CheckNull(nameof(builder));
            to_compare.CheckNull(nameof(to_compare));

            builder.AddValidator(new CompareValidator<TProp>(
                builder.Rule.Property.Name,
                to_compare, comparer, 
                CompareValidationType.Less,
                CompareValidationStrictType.Strict,
                to_str));

            return builder;
        }

        public static IRuleBuilder<T, TProp> LessThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, string> to_str = null) where TProp : IComparable<TProp>
        {
            return builder.LessThan(to_compare, (x, y) => x.CompareTo(y), to_str);
        }

        public static IRuleBuilder<T, TProp> LessOrEqualThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, TProp, int> comparer, Func<TProp, string> to_str = null)
        {
            builder.CheckNull(nameof(builder));
            to_compare.CheckNull(nameof(to_compare));

            builder.AddValidator(new CompareValidator<TProp>(
                builder.Rule.Property.Name,
                to_compare, comparer,
                CompareValidationType.Less,
                CompareValidationStrictType.Mild,
                to_str));

            return builder;
        }

        public static IRuleBuilder<T, TProp> LessOrEqualThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, string> to_str = null) where TProp : IComparable<TProp>
        {
            return builder.LessOrEqualThan(to_compare, (x, y) => x.CompareTo(y), to_str);
        }

        public static IRuleBuilder<T, TProp> GreaterThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, TProp, int> comparer, Func<TProp, string> to_str = null)
        {
            builder.CheckNull(nameof(builder));
            to_compare.CheckNull(nameof(to_compare));

            builder.AddValidator(new CompareValidator<TProp>(
                builder.Rule.Property.Name,
                to_compare, comparer,
                CompareValidationType.Greater,
                CompareValidationStrictType.Strict,
                to_str));

            return builder;
        }

        public static IRuleBuilder<T, TProp> GreaterThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, string> to_str = null) where TProp : IComparable<TProp>
        {

            return builder.GreaterThan(to_compare, (x, y) => x.CompareTo(y), to_str);
        }

        public static IRuleBuilder<T, TProp> GreaterOrEqualThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, TProp, int> comparer, Func<TProp, string> to_str = null)
        {
            builder.CheckNull(nameof(builder));
            to_compare.CheckNull(nameof(to_compare));

            builder.AddValidator(new CompareValidator<TProp>(
                builder.Rule.Property.Name,
                to_compare, comparer,
                CompareValidationType.Greater,
                CompareValidationStrictType.Mild,
                to_str));

            return builder;
        }

        public static IRuleBuilder<T, TProp> GreaterOrEqualThan<T, TProp>(this IRuleBuilder<T, TProp> builder, TProp to_compare, Func<TProp, string> to_str = null) where TProp : IComparable<TProp>
        {
            return builder.GreaterOrEqualThan(to_compare, (x, y) => x.CompareTo(y), to_str);
        }
        #endregion

        #region Strings
        public static IRuleBuilder<T, string> Length<T>(this IRuleBuilder<T, string> builder, int exact_len) 
        {
            builder.CheckNull(nameof(builder));

            builder.AddValidator(new EnumerableExactLengthValidator<char>(builder.Rule.Property.Name, exact_len));

            return builder;
        }

        public static IRuleBuilder<T, string> Length<T>(this IRuleBuilder<T, string> builder, int min, int max)
        {
            builder.CheckNull(nameof(builder));

            builder.AddValidator(new EnumerableLengthValidator<char>(builder.Rule.Property.Name, min, max));

            return builder;
        }

        public static IRuleBuilder<T, string> Matches<T>(this IRuleBuilder<T, string> builder, Regex regex)
        {
            builder.CheckNull(nameof(builder));
            regex.CheckNull(nameof(regex));

            builder.AddValidator(new MatchesValidator(builder.Rule.Property.Name, regex));

            return builder;
        }

        public static IRuleBuilder<T, string> Matches<T>(this IRuleBuilder<T, string> builder, string expression)
        {
            builder.CheckNull(nameof(builder));
            expression.CheckNull(nameof(expression));

            builder.AddValidator(new MatchesValidator(builder.Rule.Property.Name, expression));

            return builder;
        }

        public static IRuleBuilder<T, string> Matches<T>(this IRuleBuilder<T, string> builder, string expression, RegexOptions options)
        {
            builder.CheckNull(nameof(builder));
            expression.CheckNull(nameof(expression));

            builder.AddValidator(new MatchesValidator(builder.Rule.Property.Name, expression, options));

            return builder;
        }
        #endregion

        #region Enumerables
        public static IRuleBuilder<T, IEnumerable<TProp>> Length<T, TProp>(this IRuleBuilder<T, IEnumerable<TProp>> builder, int exact_len)
        {
            builder.CheckNull(nameof(builder));

            builder.AddValidator(new EnumerableExactLengthValidator<TProp>(builder.Rule.Property.Name, exact_len));

            return builder;
        }

        public static IRuleBuilder<T, IEnumerable<TProp>> Length<T, TProp>(this IRuleBuilder<T, IEnumerable<TProp>> builder, int min, int max)
        {
            builder.CheckNull(nameof(builder));

            builder.AddValidator(new EnumerableLengthValidator<TProp>(builder.Rule.Property.Name, min, max));

            return builder;
        }
        #endregion

        #region Custom
        public static IRuleBuilder<T, TProp> Must<T, TProp>(this IRuleBuilder<T, TProp> builder, Predicate<TProp> predicate)
        {
            builder.CheckNull(nameof(builder));
            predicate.CheckNull(nameof(predicate));

            builder.AddValidator(new SpecifiedConditionValidator<TProp>(builder.Rule.Property.Name, predicate));

            return builder;
        }

        public static IRuleBuilder<T, TProp> Must<T, TProp>(this IRuleBuilder<T, TProp> builder, Predicate<TProp> predicate, string error_text, ActionAtError actionAtError = ActionAtError.Break)
        {
            builder.CheckNull(nameof(builder));
            predicate.CheckNull(nameof(predicate));

            builder.AddValidator(predicate,
                    new ValidationError(builder.Rule.Property.Name, error_text),
                    actionAtError);

            return builder;
        }

        public static IRuleBuilder<T, TProp> MustWith<T, TProp>(this IRuleBuilder<T, TProp> builder, IValidator<TProp> validator)
        {
            builder.CheckNull(nameof(builder));
            validator.CheckNull(nameof(validator));

            foreach(var rule in validator.Rules)
            {
                foreach(var prop_validator in rule.Validators)
                {
                    builder.AddValidator(
                        x => prop_validator.IsValid(rule.Property.Value(x)),
                        new ValidationError(
                            string.Format("{0}.{1}", 
                                builder.Rule.Property.Name,
                                prop_validator.Error.PropertyName),
                            prop_validator.Error.ErrorMessage),
                        prop_validator.AtError);
                }
            }

            return builder;
        }
        #endregion

        #region Optional
        public static IRuleBuilder<T, TProp> Optional<T, TProp>(this IRuleBuilder<T, TProp> builder)
        {
            builder.CheckNull(nameof(builder));

            RequiredValidator<TProp> validator = new RequiredValidator<TProp>(builder.Rule.Property.Name);
            validator.AtError = ActionAtError.Stop;

            builder.AddValidator(validator);

            return builder;
        }

        public static IRuleBuilder<T, TProp> Optional<T, TProp>(this IRuleBuilder<T, TProp> builder, Predicate<TProp> predicate)
        {
            builder.CheckNull(nameof(builder));
            predicate.CheckNull(nameof(predicate));

            SpecifiedConditionValidator<TProp> validator = new SpecifiedConditionValidator<TProp>(builder.Rule.Property.Name, predicate);
            validator.AtError = ActionAtError.Stop;

            builder.AddValidator(validator);

            return builder;
        }
        #endregion
    }
}
