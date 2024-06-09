using MassTransit;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Consumers;

public class PagamentoProcessadoConsumer(IBus bus) : IConsumer<PagamentoProcessadoEvent>
{
    public async Task Consume(ConsumeContext<PagamentoProcessadoEvent> context)
    { 
        if(context.Message.StatusPagamento == StatusPagamento.PagamentoAprovado)
        {
            await bus.Publish(new Events.PagamentoConfirmadoEvent
            {
                PedidoId = context.Message.IdPedido
            });
        }
        
        //Futura publicacao evento de pagamento recusado
    }
}