using Microsoft.Extensions.DependencyInjection;
using WonderFood.Core.Interfaces.UseCases;
using WonderFood.UseCases.UseCases;

namespace WonderFood.UseCases;

public static class DependecyInjection
{
    public static void AddUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<IClienteUseCases, ClienteUseCases>();
        services.AddScoped<IProdutoUseCases, ProdutoUseCases>();
        services.AddScoped<IPedidoUseCases, PedidoUseCases>();
    }
}

