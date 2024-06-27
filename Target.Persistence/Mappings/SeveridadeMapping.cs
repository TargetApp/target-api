using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings;

public class SeveridadeMapping : IEntityTypeConfiguration<Severidade>
{
    public void Configure(EntityTypeBuilder<Severidade> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Description).HasColumnName("DESCRIPTION");

        builder.ToTable("SEVERITY");
    }
}   
