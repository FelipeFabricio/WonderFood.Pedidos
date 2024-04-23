using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarProducaoPedido;

public class ProcessarProducaoPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork, IBus bus)
    : IRequestHandler<ProcessarProducaoPedidoCommand, Unit>
{
    private readonly IBus _bus = bus;

    public async Task<Unit> Handle(ProcessarProducaoPedidoCommand request, CancellationToken cancellationToken)
    {
        StatusPedido novoStatusPedido = request.StatusPagamento switch
        {
            SituacaoPagamento.PagamentoAprovado => StatusPedido.AguardandoPreparo,
            SituacaoPagamento.PagamentoRecusado => StatusPedido.Cancelado,
            _ => throw new Exception($"Situação Pagamento inválida: {request.StatusPagamento}")
        };
        
        var pedido = await pedidoRepository.ObterPorId(request.IdPedido);
        if(pedido is null)
            throw new Exception($"Pedido não encontrado com o Id informado: {request.IdPedido}");
        
        pedido.AlterarStatusPedido(novoStatusPedido);
        
        await pedidoRepository.Atualizar(pedido);
        await unitOfWork.CommitChangesAsync();
        
        //await _bus.Publish(pagamentoSolicitadoEvent, cancellationToken);
        return Unit.Value;
    }
   
}