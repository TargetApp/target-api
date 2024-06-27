using Microsoft.EntityFrameworkCore;
using Target.Domain.Models;

namespace Target.Persistence
{
    public class QueueDbContext : DbContext
    {
        public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options) {}

        public DbSet<TargetQueue> TargetQueue { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QueueDbContext).Assembly);
        }
    }
}