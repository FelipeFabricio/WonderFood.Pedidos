using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Application.Common.Interfaces;
using WonderFood.ExternalServices.Services;

namespace WonderFood.ExternalServices;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IWonderFoodPagamentoExternal, WonderFoodPagamentoExternal>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:WonderFoodPagamentos:BaseUrl"]);
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        services.AddHttpClient<IWonderFoodProducaoExternal, WonderFoodProducaoExternal>(client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:WonderfoodProducao:BaseUrl"]);
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}