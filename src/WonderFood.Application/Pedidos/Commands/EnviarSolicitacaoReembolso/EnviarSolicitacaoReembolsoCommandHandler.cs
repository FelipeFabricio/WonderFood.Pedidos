using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.EnviarSolicitacaoReembolso;

public class EnviarSolicitacaoReembolsoCommandHandler(IBus bus, IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<EnviarSolicitacaoReembolsoCommand, Unit>
{
    public async Task<Unit> Handle(EnviarSolicitacaoReembolsoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorId(request.IdPedido);
        if(pedido is null)
            throw new ArgumentException("Pedido não encontrado.");
        
        pedido.AlterarStatusPedido(StatusPedido.AguardandoReembolsoPagamento);
        await pedidoRepository.AtualizarStatus(pedido);
        await unitOfWork.CommitChangesAsync();
        
        await bus.Publish(new ReembolsoSolicitadoEvent
        {
            IdPedido = request.IdPedido
        });

        return Unit.Value;
    }
}