using Microsoft.Extensions.DependencyInjection;

namespace Target.Persistence.Config;

public static class TargetDependencyInjectionConfig
{
    public static IServiceCollection TargetResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<TargetDbContext>();

        return services;
    }
}