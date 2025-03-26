using CsvHelper;
using Domain;
using System.Globalization;

namespace Persistence;

internal sealed class PremiumsReader
{
    private const string _path = "./Data/Premiums.csv";


    private static Task<List<Premiums>> LoanPremiumsAsync(CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(_path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<PremiumsRecordMap>();

        return csv.GetRecordsAsync<PremiumsRecord>(cancellationToken);
    }

    private static IAsyncEnumerable<PremiumsRecord> LoadRawaDataAsync(
        CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(_path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<PremiumsRecordMap>();

        return csv.GetRecordsAsync<PremiumsRecord>(cancellationToken);
    }
}
