using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings;

public class QueueMapping : IEntityTypeConfiguration<TargetQueue>
{
    public void Configure(EntityTypeBuilder<TargetQueue> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .IsRequired();

        builder.Property(x => x.ImageId)
            .HasColumnName("IMAGE_ID")
            .IsRequired();

        builder.Property(x => x.ModelId)
            .HasColumnName("MODEL_ID")
            .IsRequired();

        builder.Property(x => x.ClassificationReportId)
            .HasColumnName("CLASSIFICATION_REPORT_ID")
            .IsRequired();

        builder.Property(x => x.SegmentationReportId)
            .HasColumnName("SEGMENTATION_REPORT_ID");

        builder.Property(x => x.Image)
            .HasColumnName("IMAGE")
            .HasColumnType("mediumblob")
            .IsRequired();

        builder.Property(x => x.GenerateMask)
            .HasColumnName("GENERATE_MASK");

        builder.ToTable("PROCESSING_QUEUE", "QUEUE");
    }

}
