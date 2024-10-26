using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Entidades.Projetos.Tarefas.Atualizacoes
{
    internal class AtualizacaoTarefaConfig : IEntityTypeConfiguration<AtualizacaoTarefa>
    {
        public void Configure(EntityTypeBuilder<AtualizacaoTarefa> builder)
        {
            builder.ToTable("ATUALIZACAOTAREFA");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id);

            builder.Property(x => x.Descricao)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.DataAlteracao)
                   .HasColumnType("DATE")
                   .IsRequired();

            builder.Property(x => x.CodigoUsuario)
                  .IsRequired();

            builder.HasOne(x => x.Usuario)
                   .WithMany()
                   .HasForeignKey(x => x.CodigoUsuario)
                   .IsRequired();

            builder.Property(x => x.IdTarefa)
                   .IsRequired();

            builder.HasOne(x => x.Tarefa)
                   .WithMany(x => x.AtualizacaoTarefa)
                   .HasForeignKey(x => x.IdTarefa)
                   .IsRequired();
        }
    }
}