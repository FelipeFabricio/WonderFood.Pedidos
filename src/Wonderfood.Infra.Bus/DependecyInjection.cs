using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Core.Extensions;
using Wonderfood.Infra.Bus.Consumers;
using Wonderfood.Infra.Bus.Settings;
using Wonderfood.Models.Events;

namespace Wonderfood.Infra.Bus;

public static class DependecyInjection
{
    public static void AddAzureServiceBusServices(this IServiceCollection services)
    {
        var settings = services.CarregarSettings<AzureServiceBusSettings>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PagamentosProcessadosConsumer>();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(settings.ConnectionString,
                    h => { h.TransportType = ServiceBusTransportType.AmqpWebSockets; });

                cfg.ReceiveEndpoint(settings.Queues.PagamentosProcessados, e =>
                {
                    e.ConfigureConsumer<PagamentosProcessadosConsumer>(context);
                    e.UseMessageRetry(retryConfig => { retryConfig.Interval(3, TimeSpan.FromSeconds(5)); });
                    EndpointConvention.Map<PagamentoProcessadoEvent>(e.InputAddress);
                });
                
                cfg.ReceiveEndpoint(settings.Queues.PagamentosSolicitados, e =>
                {
                    e.UseMessageRetry(retryConfig => { retryConfig.Interval(3, TimeSpan.FromSeconds(5)); });
                    EndpointConvention.Map<PagamentoSolicitadoEvent>(e.InputAddress);
                });

                cfg.UseServiceBusMessageScheduler();
            });
        });
    }
}