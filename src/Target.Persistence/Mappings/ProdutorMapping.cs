using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class ProdutorMapping : IEntityTypeConfiguration<Produtor>
    {
        public void Configure(EntityTypeBuilder<Produtor> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.CoffeeType)
                .IsRequired()
                .HasColumnName("COFFEE_TYPE");

            builder.Property(p => p.PropertySize)
                .HasColumnName("PROPERTY_SIZE");

            builder.Property(p => p.Production)
                .HasColumnName("PRODUCTION");

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnName("USER_ID");

            builder.ToTable("PRODUCER");
        }
    }
}