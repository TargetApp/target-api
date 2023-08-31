using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target.Domain.Models;

namespace Target.Persistence.Mappings
{
    public class ProdutorMapping : IEntityTypeConfiguration<Produtor>
    {
        public void Configure(EntityTypeBuilder<Produtor> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.TipoCafe)
                .IsRequired()
                .HasColumnName("TIPO_CAFE");

            builder.Property(p => p.TamanhoPropriedade)
                .HasColumnName("TAMANHO_PROPRIEDADE");

            builder.Property(p => p.Producao)
                .HasColumnName("PRODUCAO");

            builder.Property(p => p.UsuarioId)
                .IsRequired()
                .HasColumnName("USUARIO_ID");

            builder.ToTable("PRODUTOR");
        }
    }
}