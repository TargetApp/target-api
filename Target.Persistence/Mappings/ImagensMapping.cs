using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class ImagensMapping : IEntityTypeConfiguration<Imagens>
    {
        public void Configure(EntityTypeBuilder<Imagens> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Filename)
                .IsRequired()
                .HasColumnName("FILENAME");

            builder.Property(p => p.UploadedAt)
                .HasColumnName("UPLOADED_AT");

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnName("USER_ID");

            builder.ToTable("IMAGE");
        }
    }
}