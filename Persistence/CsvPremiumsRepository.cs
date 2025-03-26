using CsvHelper;
using Domain;
using System.Collections;
using System.Globalization;
using System.Linq.Expressions;

namespace Persistence;

internal sealed class PremiumsRepository : IReadOnlyRepository<Premiums>
{
    private readonly string _path;

    public Type ElementType
    { get; }
    public Expression Expression { get; }
    public IQueryProvider Provider { get; }

    public IEnumerator<Premiums> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private async Task<List<Premiums>> LoadDataAsync(
        CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(_path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<PremiumsRecordMap>();

        List<Premiums> premiums = [];
        var records = csv.GetRecordsAsync<PremiumsRecord>(cancellationToken);
        await foreach (var record in records.WithCancellation(cancellationToken))
        {
            premiums.Add(record.ToPremiums());
        }

        return premiums;
    }


}
