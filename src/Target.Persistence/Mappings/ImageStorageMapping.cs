using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings;

public class ImageStorageMapping : IEntityTypeConfiguration<ImageStorage>
{
    public void Configure(EntityTypeBuilder<ImageStorage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .IsRequired();

        builder.Property(x => x.Data)
            .HasColumnName("DATA")
            .HasColumnType("mediumblob")
            .IsRequired();
            
        builder.ToTable("IMAGE", "STORAGE");
    }

}
