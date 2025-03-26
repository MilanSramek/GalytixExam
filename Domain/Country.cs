namespace Domain;

public readonly struct Country : IEquatable<Country>
{
    private readonly string _value;

    public Country(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        _value = value.ToLower();
    }

    public bool Equals(Country other) => _value == other._value;

    public override bool Equals(object? obj) => obj is Country other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();
}
