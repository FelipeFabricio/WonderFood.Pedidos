using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Application.Pedidos.Commands.AlterarStatus;

public class AlterarStatusPedidoCommandHandler(IPedidoRepository pedidoRepository, 
    IUnitOfWork unitOfWork, IBus bus) :  IRequestHandler<AlterarStatusPedidoCommand, Unit>
{
    public async Task<Unit> Handle(AlterarStatusPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorNumeroPedido(request.alteracaoStatus.numeroPedido);
        if(pedido is null)
            throw new ArgumentException($"Pedido não encontrado com o número informado: {request.alteracaoStatus.numeroPedido}");
        
        pedido.AlterarStatusPedido(request.alteracaoStatus.status);
        await pedidoRepository.AtualizarStatus(pedido);
        await unitOfWork.CommitChangesAsync();

        await AvancarPedidoSaga(request, pedido);
        
        return Unit.Value;
    }

    private async Task AvancarPedidoSaga(AlterarStatusPedidoCommand request, Pedido pedido)
    {
        switch (request.alteracaoStatus.status)
        {
            case StatusPedido.PreparoIniciado:
                await bus.Publish(new Events.ProducaoPedidoIniciadaEvent
                {
                    PedidoId = pedido.Id
                });                     
                break;
            
            case StatusPedido.ProntoParaRetirada:
                await bus.Publish(new Events.ProducaoPedidoConcluidaEvent
                {
                    PedidoId = pedido.Id
                });
                break;
            
            case StatusPedido.PedidoRetirado:
                await bus.Publish(new Events.PedidoRetiradoEvent
                {
                    PedidoId = pedido.Id
                });
                break;
            
            //implementar pedido cancelado
        }
    }
}