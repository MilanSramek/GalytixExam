namespace Domain;

public sealed class Premiums
{
    public Guid Id { get; }

    public Country Country { get; }

    public LineOfBusiness LineOfBusiness { get; }

    public IReadOnlyCollection<YearValue> YearValues { get; }

    public Premiums(
        Guid id,
        Country country,
        LineOfBusiness lineOfBusiness,
        IEnumerable<YearValue> yearValues)
    {
        Id = id;
        Country = country;
        LineOfBusiness = lineOfBusiness;
        YearValues = yearValues is { }
            ? [.. yearValues]
            : throw new ArgumentNullException(nameof(yearValues));
    }
}
