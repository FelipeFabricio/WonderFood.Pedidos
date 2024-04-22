using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Infra.Bus.Consumers;
using Wonderfood.Infra.Bus.Settings;
using WonderFood.Application.Extensions;

namespace Wonderfood.Infra.Bus;

public static class DependencyInjection
{
    public static void AddAzureServiceBusServices(this IServiceCollection services)
    {
        var settings = services.CarregarSettings<AzureServiceBusSettings>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PagamentosSolicitadosConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(settings.ConnectionString,
                    h => { h.TransportType = ServiceBusTransportType.AmqpWebSockets; });

                cfg.ConfigureEndpoints(context);
                cfg.UseServiceBusMessageScheduler();
            });
        });
    }
}