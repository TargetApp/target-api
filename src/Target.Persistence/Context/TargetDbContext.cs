using Microsoft.EntityFrameworkCore;

namespace Target.Persistence
{
    public class TargetDbContext : DbContext
    {
        public TargetDbContext(DbContextOptions<TargetDbContext> options) : base(options) {}

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TargetDbContext).Assembly);
        }
    }
}