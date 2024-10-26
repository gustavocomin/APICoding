using Domain.Projetos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Entidades.Projetos
{
    internal class ProjetoConfig : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.ToTable("PROJETO");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id);

            builder.Property(x => x.DataCriacao)
                   .HasColumnType("date")
                   .IsRequired();

            builder.HasMany(x => x.Tarefas)
               .WithOne(x => x.Projeto)
               .HasForeignKey(x => x.IdProjeto)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}