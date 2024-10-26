using Domain.Projetos;
using Domain.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Comentarios;
using Domain.Usuarios;
using IoC.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Config.Db;

namespace Ioc.Test.Repositorio;

public class RepositoryServiceTests
{
    [Fact]
    public void AddRepositoryServices_RegistersAllServices()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DataContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        services.AddRepositoryServices();
        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IRepUsuario>());
        Assert.NotNull(serviceProvider.GetService<IRepProjeto>());
        Assert.NotNull(serviceProvider.GetService<IRepTarefa>());
        Assert.NotNull(serviceProvider.GetService<IRepComentario>());
        Assert.NotNull(serviceProvider.GetService<IRepAtualizacaoTarefa>());
    }
}