using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Azure.Security.KeyVault.Secrets;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Infra.Bus.Consumers;

namespace WonderFood.Infra.Bus;

public static class DependencyInjection
{
    public static void AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = GetConnectionString(configuration);
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PagamentosSolicitadosConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connectionString,
                    h => { h.TransportType = ServiceBusTransportType.AmqpWebSockets; });

                cfg.ConfigureEndpoints(context);
                cfg.UseServiceBusMessageScheduler();
            });
        });
    }
    
    private static string GetConnectionString(IConfiguration configuration)
    {
        var secretClient = new SecretClient(new Uri(configuration["AzureKeyVaultUri"]!), new DefaultAzureCredential());
        return secretClient.GetSecret("wdf-pedidos-bus-connection").Value.Value;
    }
}
