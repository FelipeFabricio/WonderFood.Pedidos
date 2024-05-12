using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.EnviarParaProducao;

public class EnviarPedidoProducaoCommandHandler(IMapper mapper, IPedidoRepository pedidoRepository,
    IWonderFoodProducaoExternal wonderFoodProducaoExternal, IUnitOfWork unitOfWork) : IRequestHandler<EnviarPedidoProducaoCommand, Unit>
{
    public async Task<Unit> Handle(EnviarPedidoProducaoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorId(request.Pedido.Id);
        if (pedido is null)
            throw new ArgumentException($"Pedido não encontrado com o Id informado: {request.Pedido.Id}");
        
        pedido.AlterarStatusPedido(StatusPedido.AguardandoPreparo);
        await pedidoRepository.AtualizarStatus(pedido);
        await unitOfWork.CommitChangesAsync();
        
        var produtosPedido = mapper.Map<List<ProdutosPedido>>(pedido.Produtos);
        await wonderFoodProducaoExternal.EnviarParaProducao(new IniciarProducaoCommand
        {
            IdPedido = pedido.Id,
            NumeroPedido = pedido.NumeroPedido,
            Observacao = pedido.Observacao,
            Status = pedido.Status,
            Produtos = produtosPedido
        });

        return Unit.Value;
    }
}