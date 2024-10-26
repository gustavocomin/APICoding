using Application.Projetos;
using Application.Projetos.Tarefas;
using Application.Relatorios.Desempenho;
using Domain.Projetos;
using Domain.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Comentarios;
using Domain.Usuarios;
using IoC.Aplicacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Config.Db;
using Repository.Entidades.Projetos;
using Repository.Entidades.Projetos.Tarefas;
using Repository.Entidades.Projetos.Tarefas.Atualizacoes;
using Repository.Entidades.Projetos.Tarefas.Comentarios;
using Repository.Entidades.Usuarios;

namespace Ioc.Test.Aplicacao;
public class ApplicationServiceTests
{
    [Fact]
    public void AddApplicationServices_RegistersAllServices()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DataContext>(options =>
           options.UseInMemoryDatabase("TestDatabase"));

        services.AddScoped<IRepProjeto, RepProjeto>();
        services.AddScoped<IRepTarefa, RepTarefa>();
        services.AddScoped<IRepAtualizacaoTarefa, RepAtualizacaoTarefa>();
        services.AddScoped<IRepUsuario, RepUsuario>();
        services.AddScoped<IRepComentario, RepComentario>();


        services.AddApplicationServices();
        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IAplicProjeto>());
        Assert.NotNull(serviceProvider.GetService<IAplicTarefa>());
        Assert.NotNull(serviceProvider.GetService<IAplicRelDesempenho>());
    }
}