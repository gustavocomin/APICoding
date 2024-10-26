using Domain.Projetos.Models;
using Domain.Projetos.Tarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Test.Entidades.Projetos.Tarefas
{
    public class TarefaConfigTests
    {
        private class TestDataContext(DbContextOptions<TestDataContext> options) : DbContext(options)
        {
            public DbSet<Tarefa> Tarefas { get; set; }
        }

        [Fact]
        public void Configure_TarefaConfig_ConfiguresEntityCorrectly()
        {
            var options = new DbContextOptionsBuilder<TestDataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var context = new TestDataContext(options))
            {
                context.Database.EnsureCreated();

                var entityType = context.Model.FindEntityType(typeof(Tarefa));
                var tableName = entityType.GetTableName().ToUpper();
                var key = entityType.FindPrimaryKey();
                var foreignKeys = entityType.GetForeignKeys();

                Assert.Equal("TAREFA", tableName);
                Assert.NotNull(key);
                Assert.Equal("ID", key.Properties.First().GetColumnName().ToUpper());
                Assert.Contains(foreignKeys, fk => fk.PrincipalEntityType.ClrType == typeof(Projeto));
                Assert.Contains(foreignKeys, fk => fk.Properties.First().GetColumnName().ToUpper() == "PROJETOID");
            }
        }
    }
}