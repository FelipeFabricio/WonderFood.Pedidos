using MassTransit;
using Serilog;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Consumers;

public class StatusPedidoAlteradoConsumer(IBus bus) : IConsumer<StatusPedidoAlteradoEvent>
{
    public async Task Consume(ConsumeContext<StatusPedidoAlteradoEvent> context)
    {
        Log.Information("Evento: Status do pedido {PedidoId} alterado para {Status}", 
            context.Message.PedidoId, context.Message.Status);
        
        switch (context.Message.Status)
        {
            case StatusPedido.PreparoIniciado:
                await bus.Publish(new Events.ProducaoPedidoIniciadaEvent
                {
                    PedidoId = context.Message.PedidoId
                });                     
                break;
            
            case StatusPedido.ProntoParaRetirada:
                await bus.Publish(new Events.ProducaoPedidoConcluidaEvent
                {
                    PedidoId = context.Message.PedidoId
                });
                break;
            
            case StatusPedido.PedidoRetirado:
                await bus.Publish(new Events.PedidoRetiradoEvent
                {
                    PedidoId = context.Message.PedidoId
                });
                break;
            
            case StatusPedido.Cancelado:
                await bus.Publish(new Events.PedidoCanceladoEvent()
                {
                    PedidoId = context.Message.PedidoId,
                    MotivoCancelamento = context.Message.MotivoCancelamento
                });
                break;
        }
    }
}