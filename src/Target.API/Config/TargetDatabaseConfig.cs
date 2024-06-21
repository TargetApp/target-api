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
                context.UseMySql(
                    configuration.GetConnectionString("Database"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Database"))
                );
            });

            services.AddDbContext<StorageDbContext>(context =>
            {
                context.UseMySql(
                    configuration.GetConnectionString("Storage"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Storage"))
                );
            });

            services.AddDbContext<QueueDbContext>(context =>
            {
                context.UseMySql(
                    configuration.GetConnectionString("Queue"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Queue"))
                );
            });

            return services;
        }
    }
}