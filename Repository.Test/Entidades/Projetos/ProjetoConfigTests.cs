using Domain.Projetos.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Test.Entidades.Projetos
{
    public class ProjetoConfigTests
    {
        private class TestDataContext(DbContextOptions<ProjetoConfigTests.TestDataContext> options) : DbContext(options)
        {
            public DbSet<Projeto> Projetos { get; set; }
        }

        [Fact]
        public void Configure_ProjetoConfig_ConfiguresEntityCorrectly()
        {
            var options = new DbContextOptionsBuilder<TestDataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var context = new TestDataContext(options))
            {
                context.Database.EnsureCreated();

                var entityType = context.Model.FindEntityType(typeof(Projeto));
                var tableName = entityType.GetTableName().ToUpper(System.Globalization.CultureInfo.CurrentCulture);
                var key = entityType.FindPrimaryKey();
                var foreignKeys = entityType.GetForeignKeys();

                Assert.Equal("PROJETO", tableName);
                Assert.NotNull(key);
                Assert.Equal("ID", key.Properties.First().GetColumnName().ToUpper());
            }
        }
    }
}