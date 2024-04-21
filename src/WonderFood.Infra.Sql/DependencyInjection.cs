using Microsoft.Extensions.DependencyInjection;
using WonderFood.Core.Interfaces.Repository;
using WonderFood.Infra.Sql.Repositories;

namespace WonderFood.Infra.Sql
{
    public static class DependencyInjection
    {
        public static void AddInfraDataServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
        }
    }
}