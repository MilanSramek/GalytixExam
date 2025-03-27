using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class Registrations
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IReadOnlyRepository<Premiums>, CsvPremiumsRepository>();
        return services;
    }
}
