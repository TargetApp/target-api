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

            builder.Property(p => p.RegisterTypeId)
                .HasColumnName("REGISTER_TYPE_ID");

            builder.Property(p => p.AccountTypeId)
                .HasColumnName("ACCOUNT_TYPE_ID");

            builder.Property(p => p.Email)
                .HasColumnName("EMAIL");

            builder.Property(p => p.Telephone)
                .HasColumnName("TELEPHONE");

            builder.Property(p => p.Name)
                .HasColumnName("NAME");

            builder.Property(p => p.TokenLogin)
                .HasColumnName("TOKEN_LOGIN");

            builder.Property(p => p.TokenAttempts)
                .HasColumnName("TOKEN_ATTEMPTS");

            builder.Property(p => p.TokenUpdatedAt)
                .HasColumnName("TOKEN_UPDATED_AT");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("UPDATED_AT");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("CREATED_AT");

            builder.ToTable("USER");
        }
    }
}