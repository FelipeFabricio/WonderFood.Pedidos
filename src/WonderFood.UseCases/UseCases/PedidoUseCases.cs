using AutoMapper;
using MassTransit;
using WonderFood.Core.Dtos.Pedido;
using WonderFood.Core.Dtos.Produto;
using WonderFood.Core.Entities;
using WonderFood.Core.Interfaces.Repository;
using WonderFood.Core.Interfaces.UseCases;
using Wonderfood.Infra.Bus.Publishers;
using Wonderfood.Models.Enums;
using Wonderfood.Models.Events;

namespace WonderFood.UseCases.UseCases;

public class PedidoUseCases : IPedidoUseCases
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public PedidoUseCases(IPedidoRepository pedidoRepository, 
        IClienteRepository clienteRepository, 
        IProdutoRepository produtoRepository, 
        IMapper mapper, IBus bus)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _bus = bus;
    }
    
    public StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido)
    {
        var pedido = _pedidoRepository.ObterPorNumeroPedido(numeroPedido);
        return _mapper.Map<StatusPedidoOutputDto>(pedido);
    }

    public Task EnviarPedidoParaProducao(PagamentoProcessadoEvent contextMessage)
    {
        Console.WriteLine("Pedido enviado para produção");
        return Task.CompletedTask;
    }

    public void Inserir(InserirPedidoInputDto pedidoInputDto)
    {
        ValidarCliente(pedidoInputDto.ClienteId);
        ValidarProdutos(pedidoInputDto.Produtos);
        
        var pedido = _mapper.Map<Pedido>(pedidoInputDto);
        pedido.PreencherDataPedido();
        CalcularValorTotal(pedido);
        _pedidoRepository.Inserir(pedido);
        
        var pagamentoSolicitadoEvent = new PagamentoSolicitadoEvent
        {
            IdPedido = pedido.Id,
            ValorTotal = pedido.ValorTotal,
            FormaPagamento = (FormaPagamento)pedido.FormaPagamento,
            IdCliente = pedido.ClienteId,
            DataConfirmacaoPedido = pedido.DataPedido,
        };
        
        _bus.Send(pagamentoSolicitadoEvent);
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