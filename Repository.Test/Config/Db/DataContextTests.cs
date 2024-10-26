using Microsoft.EntityFrameworkCore;
using Repository.Config.Db;

namespace Repository.Test.Config.Db
{
    public class DataContextTests
    {
        private class TestDataContext : DataContext
        {
            public TestDataContext(DbContextOptions<DataContext> options) : base(options) { }
        }

        private DbContextOptions<DataContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        [Fact]
        public void CanCreateDataContext()
        {
            var options = GetDbContextOptions();

            using var context = new TestDataContext(options);

            Assert.NotNull(context);
        }


        [Fact]
        public void RecuperaConnectionString_ReturnsConnectionString()
        {
            Directory.SetCurrentDirectory(@"C:\Users\Gustavo\source\repos\Api Coding\Repository\");

            var connectionString = DataContext.RecuperaConnectionString();

            Assert.NotNull(connectionString);
        }

        [Fact]
        public void OnModelCreating_ApplyConfigurationsFromAssembly()
        {
            var options = GetDbContextOptions();
            using var context = new TestDataContext(options);

            context.Database.EnsureCreated();
        }
    }
}