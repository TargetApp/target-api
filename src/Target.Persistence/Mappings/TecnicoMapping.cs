using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class TecnicoMapping : IEntityTypeConfiguration<Tecnico>
    {
        public void Configure(EntityTypeBuilder<Tecnico> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.ProfessionalQualification)
                .IsRequired()
                .HasColumnName("PROFESSIONAL_QUALIFICATION");

            builder.Property(p => p.OccupationArea)
                .IsRequired()
                .HasColumnName("OCCUPATION_AREA");

            builder.Property(p => p.CouncilRegistration)
                .IsRequired()
                .HasColumnName("COUNCIL_REGISTRATION");

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnName("USER_ID");

            builder.Property(p => p.Description)
                .HasColumnName("DESCRIPTION");

            builder.Property(p => p.Evaluation)
                .HasColumnName("EVALUATION");

            builder.ToTable("TECHNICIAN");
        }
    }
}