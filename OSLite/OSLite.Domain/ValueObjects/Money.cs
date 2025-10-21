namespace OSLite.Domain.ValueObjects;

public record struct Money
{
    public decimal Value { get; }

    public Money(decimal value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Valor monetário não pode ser negativo");

        Value = value;
    }

    public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
    public static Money operator *(Money a, int quantity) => new(a.Value * quantity);

    public override string ToString() => Value.ToString("C");
}