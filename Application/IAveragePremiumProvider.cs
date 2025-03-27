
namespace Application;

public interface IAveragePremiumProvider
{
    Task<IEnumerable<AveragePremiumModel>> GetAveragePremiumsAsync(
        GetAveragePremiumInput input,
        CancellationToken cancellationToken);
}