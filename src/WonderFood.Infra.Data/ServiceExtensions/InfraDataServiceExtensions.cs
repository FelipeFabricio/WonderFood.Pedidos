using Microsoft.Extensions.DependencyInjection;
using WonderFood.Core.Interfaces;
using WonderFood.Infra.Data.Repositories;

namespace WonderFood.Infra.Data.ServiceExtensions
{
    public static class InfraDataServiceExtensions
    {
        public static void AddInfraDataServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
        }
    }
}