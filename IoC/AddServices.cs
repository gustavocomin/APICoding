using IoC.Aplicacao;
using IoC.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;
public static class AddServices
{
    public static void AdicionarDependencias(this IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddRepositoryServices();
    }
}