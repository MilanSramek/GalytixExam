namespace GalytixExam;

public static class Registrations
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}