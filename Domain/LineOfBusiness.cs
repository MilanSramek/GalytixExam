﻿namespace Domain;

public readonly struct LineOfBusiness : IEquatable<LineOfBusiness>
{
    private readonly string _value;

    public LineOfBusiness(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        _value = value.ToLower();
    }

    public bool Equals(LineOfBusiness other) => _value == other._value;

    public override bool Equals(object? obj) => obj is LineOfBusiness other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();

    public static bool operator ==(LineOfBusiness left, LineOfBusiness right) => left.Equals(right);

    public static bool operator !=(LineOfBusiness left, LineOfBusiness right) => !(left == right);

    public override string ToString() => _value;
}
