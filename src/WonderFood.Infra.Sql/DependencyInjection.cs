using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Infra.Sql.Clientes;
using WonderFood.Infra.Sql.Context;
using WonderFood.Infra.Sql.Pedidos;
using WonderFood.Infra.Sql.Produtos;

namespace WonderFood.Infra.Sql
{
    public static class DependencyInjection
    {
        public static void AddSqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["MYSQL_CONNECTION"];
            
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            services.AddDbContext<WonderFoodContext>(
                dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion,
                    mySqlOptions => { mySqlOptions.EnableRetryOnFailure(); }));

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<WonderFoodContext>());
            
            services.AddHealthChecks().AddMySql(connectionString);
        }
    }
}