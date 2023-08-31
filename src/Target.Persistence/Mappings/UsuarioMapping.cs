using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class UsuariosMapping : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.TipoCadastro)
                .IsRequired()
                .HasColumnName("TIPO_CADASTRO");

            builder.Property(p => p.TipoConta)
                .IsRequired()
                .HasColumnName("TIPO_CONTA");

            builder.Property(p => p.Email)
                .HasColumnName("EMAIL");

            builder.Property(p => p.Telefone)
                .IsRequired()
                .HasColumnName("TELEFONE");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnName("NOME");

            builder.ToTable("USUARIOS");
        }
    }
}