using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Infra.Bus.Consumers;

namespace WonderFood.Infra.Bus;

public static class DependencyInjection
{
    public static void AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["SERVICEBUS_CONNECTION"];
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
}
