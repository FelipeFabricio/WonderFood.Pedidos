using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.InserirPedido;

public class InserirPedidoCommandHandler : IRequestHandler<InserirPedidoCommand, Unit>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBus _bus;

    public InserirPedidoCommandHandler(IPedidoRepository pedidoRepository,
        IUnitOfWork unitOfWork, IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository, IBus bus)
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _bus = bus;
    }

    public async Task<Unit> Handle(InserirPedidoCommand request, CancellationToken cancellationToken)
    {
        await ValidarCliente(request.Pedido.ClienteId);
        var listaProdutosPedido = await PreencherListaProdutosPedido(request.Pedido.Produtos);

        var pedido = new Pedido(request.Pedido.ClienteId,
            listaProdutosPedido,
            Domain.Entities.Enums.FormaPagamento.Dinheiro);

        //TODO: Remover isso. Sendo para corrigir problema no EF ao salvar o Pedido
        foreach (var produtosPedido in pedido.Produtos)
            produtosPedido.Produto = null;
        
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

    private async Task ValidarCliente(Guid clienteId)
    {
        var cliente = await _clienteRepository.ObterClientePorId(clienteId);
        if (cliente == null) throw new Exception("Cliente não encontrado.");
    }

    //Rever possível uso do Task.WhenAll para melhorar performance
    private async Task<List<ProdutosPedido>> PreencherListaProdutosPedido( IEnumerable<InserirProdutosPedidoInputDto> produtos)
    {
        var tasks = new List<Task<Produto>>();
        var produtosValidos = new List<ProdutosPedido>();

        foreach (var produto in produtos)
        {
            var produtoEntity = await _produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            if (produtoEntity == null)
                throw new Exception($"Produto {produto.ProdutoId} não encontrado.");

            produtosValidos.Add(new ProdutosPedido
            {
                ProdutoId = produtoEntity.Id,
                Quantidade = produto.Quantidade,
                Produto = produtoEntity
            });
        }
        
        return produtosValidos;
    }
}
