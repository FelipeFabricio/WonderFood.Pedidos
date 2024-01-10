using AutoMapper;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces;

namespace WonderFood.UseCases.UseCases;

public class PedidoUseCases : IPedidoUseCases
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;
    
    public PedidoUseCases(IPedidoRepository pedidoRepository, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _mapper = mapper;
    }
    
    public IEnumerable<PedidosOutputDto> ObterPedidosEmAberto()
    {
        var pedidos = _pedidoRepository.ObterPedidosEmAberto();
        return _mapper.Map<IEnumerable<PedidosOutputDto>>(pedidos);
    }
    
    public int Inserir(InserirPedidoInputDto pedido)
    {
        throw new NotImplementedException();
    }

    public StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido)
    {
        var pedido = _pedidoRepository.ObterPorNumeroPedido(numeroPedido);
        return _mapper.Map<StatusPedidoOutputDto>(pedido);
    }
}