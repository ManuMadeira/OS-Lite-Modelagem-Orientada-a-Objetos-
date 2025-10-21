namespace OSLite.Domain.ValueObjects;

public record struct Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email não pode ser vazio", nameof(value));

        if (!value.Contains('@'))
            throw new ArgumentException("Email deve conter @", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
}