using Domain.Projetos.Models;
using Domain.Projetos.Tarefas.Atualizacoes.Models;
using Domain.Projetos.Tarefas.Comentarios.Models;
using Domain.Projetos.Tarefas.Models;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository.Entidades.Projetos;
using Repository.Entidades.Projetos.Tarefas;
using Repository.Entidades.Projetos.Tarefas.Atualizacoes;
using Repository.Entidades.Projetos.Tarefas.Comentarios;
using Repository.Entidades.Usuarios;

namespace Repository.Config.Db
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<AtualizacaoTarefa> AtualizacaoTarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(RecuperaConnectionString())
                               .EnableSensitiveDataLogging()
                               .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            }
        }

        public static string RecuperaConnectionString()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var parentDirectory = Directory.GetParent(currentDirectory).FullName;

            var apiPath = Path.Combine(parentDirectory, "Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("APICoding");

            return connectionString ?? throw new InvalidOperationException("Connection string not found.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new ProjetoConfig());
            modelBuilder.ApplyConfiguration(new TarefaConfig());
            modelBuilder.ApplyConfiguration(new ComentarioConfig());
            modelBuilder.ApplyConfiguration(new AtualizacaoTarefaConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
