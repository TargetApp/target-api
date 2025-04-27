using Microsoft.EntityFrameworkCore;
using Target.Persistence;

namespace Target.API.Config
{
    public static class TargetDatabaseConfig
    {
        public static IServiceCollection AddTargetDatabaseConfig(this IServiceCollection services, IConfiguration configuration, bool IsDevelopment)
        {
            // Configuração para o ambiente de Desenvolvimento
            services.AddDbContext<TargetDbContext>(context =>
            {
                context.UseMySql(
                    configuration.GetConnectionString("Database"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Database"))
                )
                .EnableDetailedErrors(IsDevelopment)
                .EnableSensitiveDataLogging(IsDevelopment); // Habilita o log de dados sensíveis apenas em desenvolvimento
            });

            services.AddDbContext<StorageDbContext>(context =>
            {
                context.UseMySql(
                    configuration.GetConnectionString("Storage"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Storage"))
                )
                .EnableDetailedErrors(IsDevelopment)
                .EnableSensitiveDataLogging(IsDevelopment); // Habilita o log de dados sensíveis apenas em desenvolvimento
            });

            services.AddDbContext<QueueDbContext>(context =>
            {
                context.UseMySql(
                    configuration.GetConnectionString("Queue"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("Queue"))
                )
                .EnableDetailedErrors(IsDevelopment)
                .EnableSensitiveDataLogging(IsDevelopment); // Habilita o log de dados sensíveis apenas em desenvolvimento
            });
                 
            return services;
        }
    }
}