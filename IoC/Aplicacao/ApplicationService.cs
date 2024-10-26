using Application.Projetos;
using Application.Projetos.Tarefas;
using Application.Relatorios.Desempenho;
using Application.Tarefas.Tarefas;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Aplicacao;
public static class ApplicationService
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAplicProjeto, AplicProjeto>();
        services.AddScoped<IAplicTarefa, AplicTarefa>();
        services.AddScoped<IAplicRelDesempenho, AplicRelDesempenho>();
    }
}