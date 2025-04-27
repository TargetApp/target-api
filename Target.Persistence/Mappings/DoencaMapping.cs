using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings;

public class DoencaMapping : IEntityTypeConfiguration<Doenca>
{
    public void Configure(EntityTypeBuilder<Doenca> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).HasColumnName("NAME");
        builder.Property(d => d.Treatment).HasColumnName("TREATMENT");
        builder.Property(d => d.Description).HasColumnName("DESCRIPTION");
        builder.Property(d => d.Prevention).HasColumnName("PREVENTION");

        builder.ToTable("DISEASE");
    }

}
