using CsvHelper;
using Domain;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Globalization;
using System.Linq.Expressions;

namespace Persistence;

internal sealed class CsvPremiumsRepository : IReadOnlyRepository<Premiums>
{
    private readonly string _path;
    private readonly ListAsyncQueryable<Premiums> _queryable;

    public CsvPremiumsRepository(IOptions<CsvPremiumsRepositorySettings> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _path = options.Value.Path;

        var provider = new ListAsyncProvider<Premiums>(LoadDataAsync);
        _queryable = new ListAsyncQueryable<Premiums>(provider);
    }

    public Type ElementType => _queryable.ElementType;
    public Expression Expression => _queryable.Expression;
    public IQueryProvider Provider => _queryable.Provider;

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
