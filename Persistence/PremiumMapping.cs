using Domain;

namespace Persistence;

internal static class PremiumMapping
{
    public static Premiums ToPremiums(this PremiumsRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        var yearValues = record.ExtraColumns
            .Select(ToYearValue)
            .ToList();

        return new Premiums(
            Guid.NewGuid(),
            new Country(record.Country),
            new LineOfBusiness(record.LineOfBusiness),
            yearValues
        );
    }

    private static YearValue ToYearValue(KeyValuePair<string, string> kvp)
    {
        var yearRaw = kvp.Key.AsSpan()[1..];
        var year = int.Parse(yearRaw);

        return new YearValue(year, decimal.Parse(kvp.Value));
    }
}
