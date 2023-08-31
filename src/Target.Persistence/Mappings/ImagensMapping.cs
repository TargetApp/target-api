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

            builder.Property(p => p.Hash)
                .IsRequired()
                .HasColumnName("HASH");

            builder.Property(p => p.RelatorioId)
                .IsRequired()
                .HasColumnName("RELATORIO_ID");

            builder.Property(p => p.Url)
                .IsRequired()
                .HasColumnName("URL");

            builder.Property(p => p.DataCriacao)
                .HasColumnName("DATA_CRIACAO");

            builder.ToTable("IMAGENS");
        }
    }
}