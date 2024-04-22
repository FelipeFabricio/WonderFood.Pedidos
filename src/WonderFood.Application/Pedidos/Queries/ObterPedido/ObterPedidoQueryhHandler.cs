using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Pedido;

namespace WonderFood.Application.Pedidos.Queries.ObterPedido;

public class ObterPedidoQueryhHandler : IRequestHandler<ObterPedidoQuery, PedidosOutputDto>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;

    public ObterPedidoQueryhHandler(IPedidoRepository pedidoRepository, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _mapper = mapper;
    }

    public async Task<PedidosOutputDto> Handle(ObterPedidoQuery request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorNumeroPedido(request.NumeroPedido);
        return _mapper.Map<PedidosOutputDto>(pedido);
    }
}