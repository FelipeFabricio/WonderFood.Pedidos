using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Pedido;

namespace WonderFood.Application.Pedidos.Queries.ObterPedido;

public class ObterPedidoQueryhHandler(IPedidoRepository pedidoRepository, IMapper mapper)
    : IRequestHandler<ObterPedidoQuery, PedidosOutputDto>
{
    public async Task<PedidosOutputDto> Handle(ObterPedidoQuery request, CancellationToken cancellationToken)
    {
        var pedido = await pedidoRepository.ObterPorNumeroPedido(request.NumeroPedido);
        return mapper.Map<PedidosOutputDto>(pedido);
    }
}