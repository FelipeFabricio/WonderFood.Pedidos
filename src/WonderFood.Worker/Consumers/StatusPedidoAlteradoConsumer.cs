using MassTransit;
using MediatR;
using WonderFood.Application.Pedidos.Commands.AlterarStatus;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Consumers;

public class StatusPedidoAlteradoConsumer(ISender mediator) : IConsumer<StatusPedidoAlteradoEvent>
{
    public async Task Consume(ConsumeContext<StatusPedidoAlteradoEvent> context)
    {
        var command = new AlterarStatusPedidoCommand(context.Message);
        await mediator.Send(command);
    }
}