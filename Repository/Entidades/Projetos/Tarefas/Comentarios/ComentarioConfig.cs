using Domain.Projetos.Tarefas.Comentarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Entidades.Projetos.Tarefas.Comentarios
{
    internal class ComentarioConfig : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("COMENTARIO");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id);

            builder.Property(x => x.Texto)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.IdTarefa)
                   .IsRequired();

            builder.HasOne(x => x.Tarefa)
                   .WithMany(x => x.Comentarios)
                   .HasForeignKey(x => x.IdTarefa)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
