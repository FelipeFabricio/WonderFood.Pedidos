using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.EnviarParaProducao;

public class EnviarPedidoProducaoCommandHandler(IMapper mapper,
    IWonderFoodProducaoExternal wonderFoodProducaoExternal) : IRequestHandler<EnviarPedidoProducaoCommand, Unit>
{
    public async Task<Unit> Handle(EnviarPedidoProducaoCommand request, CancellationToken cancellationToken)
    {
        var produtosPedido = mapper.Map<List<ProdutosPedido>>(request.Pedido.Produtos);
        await wonderFoodProducaoExternal.EnviarParaProducao(new IniciarProducaoCommand
        {
            IdPedido = request.Pedido.Id,
            NumeroPedido = request.Pedido.NumeroPedido,
            Observacao = request.Pedido.Observacao,
            Status = request.Pedido.Status,
            Produtos = produtosPedido
        });

        return Unit.Value;
    }
}