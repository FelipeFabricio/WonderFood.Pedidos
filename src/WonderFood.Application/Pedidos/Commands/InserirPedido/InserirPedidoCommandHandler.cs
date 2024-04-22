using AutoMapper;
using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;
using Wonderfood.Models.Enums;
using Wonderfood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.InserirPedido;

public class InserirPedidoCommandHandler : IRequestHandler<InserirPedidoCommand, Unit>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public InserirPedidoCommandHandler(IPedidoRepository pedidoRepository, 
        IUnitOfWork unitOfWork, IClienteRepository clienteRepository, 
        IProdutoRepository produtoRepository, IBus  bus, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _bus = bus;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(InserirPedidoCommand request, CancellationToken cancellationToken)
    {
        ValidarCliente(request.Pedido.ClienteId);
        ValidarProdutos(request.Pedido.Produtos);
        
        var pedido = _mapper.Map<Pedido>(request.Pedido);
        
        pedido.PreencherDataPedido();
        await CalcularValorTotal(pedido);   
        await _pedidoRepository.Inserir(pedido);
        await _unitOfWork.CommitChangesAsync();
        
        var pagamentoSolicitadoEvent = new PagamentoSolicitadoEvent
        {
            IdPedido = pedido.Id,
            ValorTotal = pedido.ValorTotal,
            FormaPagamento = (FormaPagamento)pedido.FormaPagamento,
            IdCliente = pedido.ClienteId,
            DataConfirmacaoPedido = pedido.DataPedido,
        };
        
        await _bus.Publish(pagamentoSolicitadoEvent, cancellationToken);
        return Unit.Value;
    }
      
    private async Task CalcularValorTotal(Pedido pedido)
    {
        foreach (var produto in pedido.Produtos)
        {
            var produtoEntity = await _produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            if(produtoEntity is null)
                throw new Exception($"Produto {produto.ProdutoId} não encontrado.");
            
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