using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Registrations
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAveragePremiumProvider, AveragePremiumProvider>();
        return services;
    }
}