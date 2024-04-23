using MassTransit;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.InserirPedido;

public class InserirPedidoCommandHandler(
    IPedidoRepository pedidoRepository,
    IUnitOfWork unitOfWork,
    IClienteRepository clienteRepository,
    IProdutoRepository produtoRepository,
    IBus bus)
    : IRequestHandler<InserirPedidoCommand, Unit>
{
    public async Task<Unit> Handle(InserirPedidoCommand request, CancellationToken cancellationToken)
    {
        await ValidarCliente(request.Pedido.ClienteId);
        var listaProdutosPedido = await PreencherListaProdutosPedido(request.Pedido.Produtos);

        var pedido = new Pedido(request.Pedido.ClienteId,
            listaProdutosPedido,
            Domain.Entities.Enums.FormaPagamento.Dinheiro);
        
        await pedidoRepository.Inserir(pedido);
        await unitOfWork.CommitChangesAsync();

        var pagamentoSolicitadoEvent = new PagamentoSolicitadoEvent
        {
            IdPedido = pedido.Id,
            ValorTotal = pedido.ValorTotal,
            FormaPagamento = (FormaPagamento)pedido.FormaPagamento,
            IdCliente = pedido.ClienteId,
            DataConfirmacaoPedido = pedido.DataPedido,
        };

        await bus.Publish(pagamentoSolicitadoEvent, cancellationToken);
        return Unit.Value;
    }

    private async Task ValidarCliente(Guid clienteId)
    {
        var cliente = await clienteRepository.ObterClientePorId(clienteId);
        if (cliente == null) throw new Exception("Cliente não encontrado.");
    }

    //TODO: Rever possível uso do Task.WhenAll para melhorar performance
    private async Task<List<ProdutosPedido>> PreencherListaProdutosPedido( IEnumerable<InserirProdutosPedidoInputDto> produtos)
    {
        var produtosValidos = new List<ProdutosPedido>();

        foreach (var produto in produtos)
        {
            var produtoEntity = await produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            if (produtoEntity == null)
                throw new Exception($"Produto {produto.ProdutoId} não encontrado.");

            produtosValidos.Add(new ProdutosPedido
            {
                ProdutoId = produtoEntity.Id,
                Quantidade = produto.Quantidade,
                ValorProduto = produtoEntity.Valor
            });
        }
        return produtosValidos;
    }
}
