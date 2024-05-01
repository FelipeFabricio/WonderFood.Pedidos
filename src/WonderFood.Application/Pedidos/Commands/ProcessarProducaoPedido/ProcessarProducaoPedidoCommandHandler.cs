using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarProducaoPedido;

public class ProcessarProducaoPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<ProcessarProducaoPedidoCommand, Unit>
{
    public async Task<Unit> Handle(ProcessarProducaoPedidoCommand request, CancellationToken cancellationToken)
    {
        var novoStatusPedido = request.StatusPagamento switch
        {
            SituacaoPagamento.PagamentoAprovado => StatusPedido.PagamentoAprovado,
            SituacaoPagamento.PagamentoRecusado => StatusPedido.PagamentoRecusado,
            _ => throw new ArgumentException($"Situação Pagamento inválida: {request.StatusPagamento}")
        };
        
        var pedido = await pedidoRepository.ObterPorId(request.IdPedido);
        if(pedido is null)
            throw new ArgumentException($"Pedido não encontrado com o Id informado: {request.IdPedido}");
        
        pedido.AlterarStatusPedido(novoStatusPedido);
        
        await pedidoRepository.Atualizar(pedido);
        await unitOfWork.CommitChangesAsync();
        
        return Unit.Value;
    }
}