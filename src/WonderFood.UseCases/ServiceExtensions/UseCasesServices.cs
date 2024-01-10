using Microsoft.Extensions.DependencyInjection;
using WonderFood.Core.Interfaces;
using WonderFood.UseCases.Cliente;
using WonderFood.UseCases.UseCases;

namespace WonderFood.UseCases.ServiceExtensions;

public static class UseCasesServices
{
    public static void AddUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<IClienteUseCases, ClienteUseCases>();
        services.AddScoped<IProdutoUseCases, ProdutoUseCases>();
        services.AddScoped<IPedidoUseCases, PedidoUseCases>();
    }
}

