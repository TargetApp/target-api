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
                .HasColumnName("TIPO_CADASTRO");

            builder.Property(p => p.TipoConta)
                .HasColumnName("TIPO_CONTA");

            builder.Property(p => p.Email)
                .HasColumnName("EMAIL");

            builder.Property(p => p.Telefone)
                .HasColumnName("TELEFONE");

            builder.Property(p => p.Nome)
                .HasColumnName("NOME");

            builder.Property(p => p.TokenLogin)
                .HasColumnName("TOKEN_LOGIN");

            builder.Property(p => p.TokenTentativas)
                .HasColumnName("TOKEN_TENTATIVAS");

            builder.Property(p => p.DataAtualizacaoToken)
                .HasColumnName("DATA_ATUALIZACAO_TOKEN");

            builder.Property(p => p.DataAtualizacaoUsuario)
                .HasColumnName("DATA_ATUALIZACAO_USUARIO");

            builder.Property(p => p.DataCriacaoUsuario)
                .HasColumnName("DATA_CRIACAO_USUARIO");

            builder.ToTable("USUARIOS");
        }
    }
}