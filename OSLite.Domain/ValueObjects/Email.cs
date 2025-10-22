using System;
using System.Text.RegularExpressions;

namespace OSLite.Domain.ValueObjects
{
    public readonly record struct Email
    {
        private static readonly Regex SimpleEmail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.", nameof(value));

            if (!SimpleEmail.IsMatch(value))
                throw new ArgumentException("Email format is invalid.", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;
    }
}
