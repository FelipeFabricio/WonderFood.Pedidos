using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Application.Common.Interfaces;
using WonderFood.ExternalServices.Services;

namespace WonderFood.ExternalServices;

public static class DependencyInjection
{
    public static void AddExternalServices(this IServiceCollection services)
    {
        services.AddHttpClient<IWonderFoodPagamentoExternal, WonderFoodPagamentoExternal>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        services.AddHttpClient<IWonderFoodProducaoExternal, WonderFoodProducaoExternal>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<IWonderFoodPagamentoExternal, WonderFoodPagamentoExternal>();
        services.AddScoped<IWonderFoodProducaoExternal, WonderFoodProducaoExternal>();
    }
}