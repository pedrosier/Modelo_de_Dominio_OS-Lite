using System;

namespace OSLite.Domain.ValueObjects
{
    public readonly record struct Money
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Money value cannot be negative.");

            Value = value;
        }

        public decimal Multiply(int quantity) => Value * quantity;

        public override string ToString() => Value.ToString("C");
    }
}
