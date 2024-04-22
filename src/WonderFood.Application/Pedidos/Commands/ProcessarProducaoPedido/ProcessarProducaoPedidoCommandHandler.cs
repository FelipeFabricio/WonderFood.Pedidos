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
            SituacaoPagamento.PagamentoAprovado => StatusPedido.EmPreparacao,
            SituacaoPagamento.PagamentoRecusado => StatusPedido.Cancelado,
            _ => throw new ArgumentOutOfRangeException(nameof(request.StatusPagamento), $"Situação Pagamento inválida: {request.StatusPagamento}")
        };
        
        await _pedidoRepository.AtualizarStatusPedido(request.IdPedido, novoStatusPedido);
        await _unitOfWork.CommitChangesAsync();
        
        //await _bus.Publish(pagamentoSolicitadoEvent, cancellationToken);
        return Unit.Value;
    }
   
}