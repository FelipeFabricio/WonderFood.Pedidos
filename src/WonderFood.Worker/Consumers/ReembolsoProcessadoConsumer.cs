using MassTransit;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Consumers;

public class ReembolsoProcessadoConsumer(IBus bus) : IConsumer<ReembolsoProcessadoEvent>
{
    public async Task Consume(ConsumeContext<ReembolsoProcessadoEvent> context)
    {
        if (context.Message.StatusReembolso == StatusPagamento.ReembolsoAprovado)
        {
            await bus.Publish(new Events.ReembolsoEfetuadoEvent
            {
                PedidoId = context.Message.IdPedido
            });
        }
        else
        {
            await bus.Publish(new Events.ReembolsoRecusadoEvent()
            {
                PedidoId = context.Message.IdPedido
            });
        }
    }
}