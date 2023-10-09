using Microsoft.EntityFrameworkCore;
using Target.Domain.Models;
using Target.Persistence.Mappings;

namespace Target.Persistence
{
    public class TargetDbContext : DbContext
    {
        public TargetDbContext(DbContextOptions<TargetDbContext> options) : base(options) {}

        public DbSet<Imagens> Imagens { get; set; }
        public DbSet<Relatorio> Relatorio { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Produtor> Produtor { get; set; } 
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TargetDbContext).Assembly);
        }
    }
}