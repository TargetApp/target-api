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

            builder.Property(p => p.FormacaoProfissional)
                .IsRequired()
                .HasColumnName("FORMACAO_PROFISSIONAL");

            builder.Property(p => p.AreaAtuacao)
                .IsRequired()
                .HasColumnName("AREA_ATUACAO");

            builder.Property(p => p.RegistroConselho)
                .IsRequired()
                .HasColumnName("REGISTRO_CONSELHO");

            builder.Property(p => p.UsuarioId)
                .IsRequired()
                .HasColumnName("USUARIO_ID");

            builder.ToTable("TECNICO");
        }
    }
}