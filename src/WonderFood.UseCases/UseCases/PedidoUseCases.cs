using AutoMapper;
using WonderFood.Core.Dtos.Pedido;
using WonderFood.Core.Dtos.Produto;
using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces.Repository;
using WonderFood.Core.Interfaces.UseCases;

namespace WonderFood.UseCases.UseCases;

public class PedidoUseCases : IPedidoUseCases
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public PedidoUseCases(IPedidoRepository pedidoRepository, 
        IClienteRepository clienteRepository, 
        IProdutoRepository produtoRepository, 
        IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
   
    public void Inserir(InserirPedidoInputDto pedidoInputDto)
    {
        ValidarCliente(pedidoInputDto.ClienteId);
        ValidarProdutos(pedidoInputDto.Produtos);
        
        var pedido = _mapper.Map<Pedido>(pedidoInputDto);
        pedido.PreencherDataPedido();
        CalcularValorTotal(pedido);
        _pedidoRepository.Inserir(pedido);
    }
    
    public StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido)
    {
        var pedido = _pedidoRepository.ObterPorNumeroPedido(numeroPedido);
        return _mapper.Map<StatusPedidoOutputDto>(pedido);
    }
    
    private void CalcularValorTotal(Pedido pedido)
    {
        foreach (var produto in pedido.Produtos)
        {
            var produtoEntity = _produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            pedido.ValorTotal += produtoEntity.Valor * produto.Quantidade;
        }
    }
    
    private void ValidarCliente(Guid clienteId)
    {
        var cliente = _clienteRepository.ObterClientePorId(clienteId);
        if (cliente == null) throw new Exception("Cliente não encontrado.");
    }
    
    private void ValidarProdutos(IEnumerable<InserirProdutosPedidoInputDto> produtos)
    {
        foreach (var produto in produtos)
        {
            var produtoEntity = _produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            if (produtoEntity == null) throw new Exception($"Produto {produto.ProdutoId} não encontrado.");
        }
    }
}