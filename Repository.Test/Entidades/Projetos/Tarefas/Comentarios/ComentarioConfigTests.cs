using Domain.Projetos.Tarefas.Comentarios.Models;
using Domain.Projetos.Tarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Test.Entidades.Projetos.Comentarios.Comentarios
{
    public class ComentarioConfigTests
    {
        private class TestDataContext(DbContextOptions<TestDataContext> options) : DbContext(options)
        {
            public DbSet<Comentario> Comentarios { get; set; }
        }

        [Fact]
        public void Configure_ComentarioConfig_ConfiguresEntityCorrectly()
        {
            var options = new DbContextOptionsBuilder<TestDataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var context = new TestDataContext(options))
            {
                context.Database.EnsureCreated();

                var entityType = context.Model.FindEntityType(typeof(Comentario));
                var tableName = entityType.GetTableName().ToUpper();
                var key = entityType.FindPrimaryKey();
                var foreignKeys = entityType.GetForeignKeys();

                Assert.Equal("COMENTARIO", tableName);
                Assert.NotNull(key);
                Assert.Equal("ID", key.Properties.First().GetColumnName().ToUpper());
                Assert.Contains(foreignKeys, fk => fk.PrincipalEntityType.ClrType == typeof(Tarefa));
                Assert.Contains(foreignKeys, fk => fk.Properties.First().GetColumnName().ToUpper() == "TAREFAID");
            }
        }
    }
}