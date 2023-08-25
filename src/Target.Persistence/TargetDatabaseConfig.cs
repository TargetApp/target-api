using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Target.Persistence
{
    public static class TargetDatabaseConfig
    {
        public static IServiceCollection AddTargetDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TargetDbContext>(context =>
            {
                context.UseSqlite(configuration.GetConnectionString("Default"));
            });
            return services;
        }
    }
}