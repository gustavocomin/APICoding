using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Entidades.Usuarios
{
    internal class UsuarioConfigTests : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id)
                   .HasColumnName("IDUSUARIO");

            builder.Property(x => x.Nome)
                   .HasColumnName("NOME")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Login)
                   .HasColumnName("LOGIN")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Acesso)
                   .HasColumnName("ACESSO")
                   .IsRequired();
        }
    }
}
