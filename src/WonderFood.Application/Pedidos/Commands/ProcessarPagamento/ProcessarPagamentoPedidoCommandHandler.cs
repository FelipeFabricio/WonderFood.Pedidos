using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.EnviarParaProducao;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarPagamento;

public class ProcessarPagamentoPedidoCommandHandler(
    IPedidoRepository pedidoRepository,
    IUnitOfWork unitOfWork,
    ISender sender
)
    : IRequestHandler<ProcessarPagamentoPedidoCommand, Unit>
{
    public async Task<Unit> Handle(ProcessarPagamentoPedidoCommand request, CancellationToken cancellationToken)
    {
        var novoStatusPedido = request.StatusPagamento switch
        {
            StatusPagamento.PagamentoAprovado => StatusPedido.PagamentoAprovado,
            StatusPagamento.PagamentoRecusado => StatusPedido.PagamentoRecusado,
            _ => throw new ArgumentException($"Situação Pagamento inválida: {request.StatusPagamento}")
        };

        var pedido = await pedidoRepository.ObterPorId(request.IdPedido);
        if (pedido is null)
            throw new ArgumentException($"Pedido não encontrado com o Id informado: {request.IdPedido}");

        pedido.AlterarStatusPedido(novoStatusPedido);

        await pedidoRepository.Atualizar(pedido);
        await unitOfWork.CommitChangesAsync();

        await sender.Send(new EnviarPedidoProducaoCommand(pedido), default);
        
        return Unit.Value;
    }
}