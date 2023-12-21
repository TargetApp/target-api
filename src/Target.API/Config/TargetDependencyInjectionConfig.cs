using Target.Persistence.Interfaces;
using Target.Persistence.Persistences;
using Target.Persistence;
using Target.Application.Interfaces;
using Target.Application.Services;
using IcmPortal.Core.Dominio.Interfaces;
using Target.API.Extensions;

namespace Target.API.Config;

public static class TargetDependencyInjectionConfig
{
    public static IServiceCollection TargetResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<TargetDbContext>();
        services.AddHttpContextAccessor();
        // Persists Dependency Injection
        services.AddScoped<IGeralPersist, GeralPersist>();
        services.AddScoped<IProdutorPersist, ProdutorPersist>();
        services.AddScoped<ITecnicoPersist, TecnicoPersist>();
        services.AddScoped<IUsuarioPersist, UsuarioPersist>();
        services.AddScoped<IRelatorioPersist, RelatorioPersist>();
        services.AddScoped<IImagemPersist, ImagemPersist>();

        // Services Dependency Injection
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITecnicoService, TecnicoService>();
        services.AddScoped<IProdutorService, ProdutorService>();
        services.AddScoped<IRelatorioService, RelatorioService>();

        services.AddScoped<IUsuarioLogado, AuthUsuarioLogado>();

        return services;
    }
}