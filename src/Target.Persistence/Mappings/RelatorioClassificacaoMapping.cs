using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class RelatorioClassificacaoMapping : IEntityTypeConfiguration<RelatorioClassificacao>
    {
        public void Configure(EntityTypeBuilder<RelatorioClassificacao> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnName("USER_ID");

            builder.Property(p => p.ImageId)    
                .IsRequired()
                .HasColumnName("IMAGE_ID");

            builder.Property(p => p.ModelId)
                .IsRequired()
                .HasColumnName("MODEL_ID");

            builder.Property(p => p.DiseaseId)
                .HasColumnName("DISEASE_ID");
            
            builder.Property(p => p.SeverityId)
                .HasColumnName("SEVERITY_ID");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("CREATED_AT");

            builder.Property(p => p.ProcessedAt)
                .HasColumnName("PROCESSED_AT");

            builder.ToTable("CLASSIFICATION_REPORT");
        }
    }
}