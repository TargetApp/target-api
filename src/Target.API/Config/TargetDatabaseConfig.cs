using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Target.Persistence;

namespace Target.API.Config
{
    public static class TargetDatabaseConfig
    {
        public static IServiceCollection AddTargetDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TargetDbContext>(context =>
            {
                context.UseMySql(configuration.GetConnectionString("TargetDatabase"), new MySqlServerVersion(new Version(8, 0, 25)));
            });
            return services;
        }
    }
}