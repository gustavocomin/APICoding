using Domain.Projetos;
using Domain.Projetos.Tarefas;
using Domain.Projetos.Tarefas.Atualizacoes;
using Domain.Projetos.Tarefas.Comentarios;
using Domain.Usuarios;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entidades.Projetos;
using Repository.Entidades.Projetos.Tarefas;
using Repository.Entidades.Projetos.Tarefas.Atualizacoes;
using Repository.Entidades.Projetos.Tarefas.Comentarios;
using Repository.Entidades.Usuarios;

namespace IoC.Repositorio;

public static class RepositoryService
{
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IRepUsuario, RepUsuario>();
        services.AddScoped<IRepProjeto, RepProjeto>();
        services.AddScoped<IRepTarefa, RepTarefa>();
        services.AddScoped<IRepComentario, RepComentario>();
        services.AddScoped<IRepAtualizacaoTarefa, RepAtualizacaoTarefa>();
    }
}