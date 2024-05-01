using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Infra.Bus.Consumers;
using WonderFood.Models.Events;

namespace WonderFood.Infra.Bus;

public static class DependencyInjection
{
    public static void AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = "sb://wonderfood.servicebus.windows.net/;SharedAccessKeyName=wonderfood-pedidos;SharedAccessKey=HYIj5W+MoepQvOE5WEQjnHsI42l/zt1dj+ASbNvQ2Js=";

        services.AddMassTransit(x =>
        {
            x.AddConsumer<PagamentosProcessadosConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connectionString,
                    h => { h.TransportType = ServiceBusTransportType.AmqpWebSockets; });

                cfg.ReceiveEndpoint("pagamentos-solicitados", e =>
                {
                    e.ConfigureConsumer<PagamentosProcessadosConsumer>(context);
                    e.UseMessageRetry(retryConfig => { retryConfig.Interval(3, TimeSpan.FromSeconds(5)); });
                    EndpointConvention.Map<PagamentoProcessadoEvent>(e.InputAddress);
                });
                
                cfg.ConfigureEndpoints(context);
                cfg.UseServiceBusMessageScheduler();
            });
        });
    }
}
