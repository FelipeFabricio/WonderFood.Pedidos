using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.IniciarProducaoPedido;
using WonderFood.Domain.Entities.Enums;
using Wonderfood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarProducaoPedido;

public class ProcessarProducaoPedidoCommandHandler : IRequestHandler<ProcessarProducaoPedidoCommand, Unit>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBus _bus;

    public ProcessarProducaoPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork, IBus bus)
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
        _bus = bus;
    }

    public async Task<Unit> Handle(ProcessarProducaoPedidoCommand request, CancellationToken cancellationToken)
    {
        StatusPedido novoStatusPedido = request.StatusPagamento switch
        {
            SituacaoPagamento.PagamentoAprovado => StatusPedido.AguardandoPreparo,
            SituacaoPagamento.PagamentoRecusado => StatusPedido.Cancelado,
            _ => throw new Exception($"Situação Pagamento inválida: {request.StatusPagamento}")
        };
        
        var pedido = await _pedidoRepository.ObterPorId(request.IdPedido);
        if(pedido is null)
            throw new Exception($"Pedido não encontrado com o Id informado: {request.IdPedido}");
        
        pedido.AlterarStatusPedido(novoStatusPedido);
        
        await _pedidoRepository.Atualizar(pedido);
        await _unitOfWork.CommitChangesAsync();
        
        //await _bus.Publish(pagamentoSolicitadoEvent, cancellationToken);
        return Unit.Value;
    }
   
}