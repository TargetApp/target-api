using Microsoft.Extensions.DependencyInjection;
using Target.Persistence.Interfaces;
using Target.Persistence.Persistences;
using Target.Persistence;
using Target.Application.Interfaces;
using Target.Application.Services;

namespace Target.Application.Config;

public static class TargetDependencyInjectionConfig
{
    public static IServiceCollection TargetResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<TargetDbContext>();

        // Persists Dependency Injection
        services.AddScoped<IGeralPersist, GeralPersist>();
        services.AddScoped<IProdutorPersist, ProdutorPersist>();
        services.AddScoped<ITecnicoPersist, TecnicoPersist>();
        services.AddScoped<IUsuarioPersist, UsuarioPersist>();
        services.AddScoped<IRelatorioPersist, RelatorioPersist>();
        services.AddScoped<IImagemPersist, ImagemPersist>();

        // Services Dependency Injection
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}