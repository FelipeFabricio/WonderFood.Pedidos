using MediatR;
using WonderFood.Domain.Dtos.Pedido;

namespace WonderFood.Application.Pedidos.Queries.ObterPedido;

public record ObterPedidoQuery(int NumeroPedido) : IRequest<PedidosOutputDto>
{
    
}