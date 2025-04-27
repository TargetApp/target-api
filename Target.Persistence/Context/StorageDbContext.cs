using Microsoft.EntityFrameworkCore;
using Target.Domain.Models;

namespace Target.Persistence
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options) {}

        public DbSet<ImageStorage> ImageStorage { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorageDbContext).Assembly);
        }
    }
}