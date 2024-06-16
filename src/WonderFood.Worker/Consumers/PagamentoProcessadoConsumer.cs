using MassTransit;
using Serilog;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Consumers;

public class PagamentoProcessadoConsumer(IBus bus) : IConsumer<PagamentoProcessadoEvent>
{
    public async Task Consume(ConsumeContext<PagamentoProcessadoEvent> context)
    {
        Log.Information("Evento: Pagamento processado para o pedido {PedidoId} - Status:{Status}", 
            context.Message.IdPedido, context.Message.StatusPagamento);
        
        if(context.Message.StatusPagamento == StatusPagamento.PagamentoAprovado)
        {
            await bus.Publish(new Events.PagamentoConfirmadoEvent
            {
                PedidoId = context.Message.IdPedido
            });
        }
        
        await bus.Publish(new Events.PagamentoRecusadoEvent()
        {
            PedidoId = context.Message.IdPedido
        });
    }
}