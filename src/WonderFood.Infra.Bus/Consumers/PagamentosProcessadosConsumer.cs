using MassTransit;
using MediatR;
using WonderFood.Application.Pedidos.Commands.ProcessarProducaoPedido;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Infra.Bus.Consumers;

public class PagamentosProcessadosConsumer : IConsumer<PagamentoProcessadoEvent>
{
    private readonly ISender _mediator;

    public PagamentosProcessadosConsumer(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PagamentoProcessadoEvent> context)
    {
        var message = new PagamentoProcessadoEvent()
        {
            IdPedido = context.Message.IdPedido,
            StatusPagamento = SituacaoPagamento.PagamentoAprovado
        };

        var command = new ProcessarProducaoPedidoCommand(message.IdPedido, message.StatusPagamento);
        await _mediator.Send(command);
    }
}
