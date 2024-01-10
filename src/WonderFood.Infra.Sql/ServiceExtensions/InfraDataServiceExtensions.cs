using Microsoft.Extensions.DependencyInjection;
using WonderFood.Core.Interfaces;
using WonderFood.Infra.Sql.Repositories;

namespace WonderFood.Infra.Sql.ServiceExtensions
{
    public static class InfraDataServiceExtensions
    {
        public static void AddInfraDataServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
        }
    }
}