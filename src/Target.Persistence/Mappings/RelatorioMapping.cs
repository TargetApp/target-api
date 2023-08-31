using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class RelatorioMapping : IEntityTypeConfiguration<Relatorio>
    {
        public void Configure(EntityTypeBuilder<Relatorio> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                .HasColumnName("NOME");

            builder.Property(p => p.DataCriacao)
                .HasColumnName("DATA_CRIACAO");

            builder.Property(p => p.UsuarioId)
                .IsRequired()
                .HasColumnName("USUARIO_ID");

            builder.ToTable("RELATORIO");
        }
    }
}