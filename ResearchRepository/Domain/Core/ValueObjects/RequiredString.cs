using System.Collections.Generic;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace ResearchRepository.Domain.Core.ValueObjects
{
    /// <summary>
    /// Represents a value object with a string that has a size limit and cannot be null or empty.
    /// </summary>
    
    public class RequiredString : ValueObject
    {
        public const int MaxLength = 200;
        public string Value { get; }
        private RequiredString(string value)
        {
            Value = value;
        }
        // for EFCore
        private RequiredString()
        {
            Value = null!;
        }
        public static Validation<ValidationError, RequiredString>
        TryCreate(string? value, int maxLength = MaxLength)
        {
            Regex rx = new Regex(@"(?i)\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\b");
            Match match = rx.Match(value);

            if (match.Success)
                return new SQLInjection("Esta string no es válida");
            if (string.IsNullOrWhiteSpace(value))
                return new IsNullOrWhitespace();
            if (value.Length > maxLength)
                return new TooLong(maxLength);
            return new RequiredString(value);
        }

        public static RequiredString CreateRequiredString(string? value)
        {
            return new RequiredString(value);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public override string ToString()
        {
            return Value;
        }
        public abstract record ValidationError;
        public record IsNullOrWhitespace : ValidationError;
        public record TooLong(int MaxLength) : ValidationError;
        public record SQLInjection(string Message) : ValidationError;
    }
}
