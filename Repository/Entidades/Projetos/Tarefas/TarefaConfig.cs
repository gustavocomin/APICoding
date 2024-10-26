using Domain.Projetos.Tarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Entidades.Projetos.Tarefas
{
    internal class TarefaConfig : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("TAREFA");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Prioridade)
               .IsRequired();

            builder.Property(x => x.Status)
               .IsRequired();

            builder.Property(x => x.IdProjeto)
                   .IsRequired();

            builder.HasOne(x => x.Projeto)
                  .WithMany(x => x.Tarefas)
                  .HasForeignKey(x => x.IdProjeto)
                  .IsRequired();

            builder.HasMany(x => x.Comentarios)
                   .WithOne(x => x.Tarefa)
                   .HasForeignKey(x => x.IdTarefa)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AtualizacaoTarefa)
                   .WithOne(x => x.Tarefa)
                   .HasForeignKey(x => x.IdTarefa)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}