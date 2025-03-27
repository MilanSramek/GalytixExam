using Domain;
using Expansions;

namespace Application;

internal sealed class AveragePremiumProvider : IAveragePremiumProvider
{
    private readonly IReadOnlyRepository<Premiums> _premiumsRepository;

    public AveragePremiumProvider(IReadOnlyRepository<Premiums> premiumsRepository)
    {
        _premiumsRepository = premiumsRepository ?? throw new ArgumentNullException(nameof(premiumsRepository));
    }

    public async Task<IEnumerable<AveragePremiumModel>> GetAveragePremiumsAsync(
        GetAveragePremiumInput input,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        IQueryable<Premiums> premiums = _premiumsRepository;
        if (input is { Country: Country country })
        {
            premiums = premiums.Where(_ => _.Country == country);
        }
        if (input is { LineOfBusinessItems: LineOfBusiness[] lineOfBusinessItems })
        {
            premiums = premiums.Where(_ => lineOfBusinessItems.Contains(_.LineOfBusiness));
        }

        var resultingPremiums = await premiums.ToListAsync(cancellationToken);
        return resultingPremiums
            .Select(_ => new AveragePremiumModel
            (
                LineOfBusiness: _.LineOfBusiness,
                Value: _.YearValues.Average(_ => _.Value)
            ));
    }
}
