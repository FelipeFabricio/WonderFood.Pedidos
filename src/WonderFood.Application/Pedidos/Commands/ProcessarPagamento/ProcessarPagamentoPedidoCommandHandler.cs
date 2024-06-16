using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.EnviarParaProducao;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarPagamento;

public class ProcessarPagamentoPedidoCommandHandler(
    IPedidoRepository pedidoRepository,
    IUnitOfWork unitOfWork,
    ISender mediator
)
    : IRequestHandler<ProcessarPagamentoPedidoCommand, Unit>
{
    public async Task<Unit> Handle(ProcessarPagamentoPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorId(request.IdPedido);
        if (pedido is null)
            throw new ArgumentException($"Pedido não encontrado com o Id informado: {request.IdPedido}");

        StatusPedido novoStatusPedido = request.StatusPagamento switch
        {
            StatusPagamento.PagamentoAprovado => StatusPedido.PagamentoAprovado,
            StatusPagamento.PagamentoRecusado => StatusPedido.Cancelado,
            _ => throw new ArgumentException("Status de pagamento inválido")
        };

        pedido.AlterarStatusPedido(novoStatusPedido);

        await pedidoRepository.AtualizarStatus(pedido);
        await unitOfWork.CommitChangesAsync();

        if (novoStatusPedido == StatusPedido.PagamentoAprovado)
            await mediator.Send(new EnviarPedidoProducaoCommand(pedido));

        return Unit.Value;
    }
}