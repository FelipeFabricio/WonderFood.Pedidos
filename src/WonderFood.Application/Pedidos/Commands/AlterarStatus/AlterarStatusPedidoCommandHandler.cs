using MediatR;
using WonderFood.Application.Common.Interfaces;

namespace WonderFood.Application.Pedidos.Commands.AlterarStatus;

public class AlterarStatusPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork) :  IRequestHandler<AlterarStatusPedidoCommand, Unit>
{
    public async Task<Unit> Handle(AlterarStatusPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorNumeroPedido(request.alteracaoStatus.NumeroPedido);
        if(pedido is null)
            throw new ArgumentException($"Pedido não encontrado com o número informado: {request.alteracaoStatus.NumeroPedido}");
        
        pedido.AlterarStatusPedido(request.alteracaoStatus.Status);
        await pedidoRepository.AtualizarStatus(pedido);
        await unitOfWork.CommitChangesAsync();
        return Unit.Value;
    }
}