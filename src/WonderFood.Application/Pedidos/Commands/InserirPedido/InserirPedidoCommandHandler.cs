using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Pedidos.Commands.InserirPedido;

public class InserirPedidoCommandHandler(
    IPedidoRepository pedidoRepository,
    IUnitOfWork unitOfWork,
    IClienteRepository clienteRepository,
    IProdutoRepository produtoRepository,
    IMapper mapper)
    : IRequestHandler<InserirPedidoCommand, PedidosOutputDto>
{
    public async Task<PedidosOutputDto> Handle(InserirPedidoCommand request, CancellationToken cancellationToken)
    {
        await ValidarCliente(request.Pedido.ClienteId);
        var listaProdutosPedido = await PreencherListaProdutosPedido(request.Pedido.Produtos);

        var pedido = new Pedido(request.Pedido.ClienteId,
            listaProdutosPedido,
            request.Pedido.FormaPagamento,
            request.Pedido.Observacao);
        
        await pedidoRepository.Inserir(pedido);
        await unitOfWork.CommitChangesAsync();

        var pedidoCadastrado = await pedidoRepository.ObterPorId(pedido.Id);
        return mapper.Map<PedidosOutputDto>(pedidoCadastrado);
    }

    private async Task ValidarCliente(Guid clienteId)
    {
        var cliente = await clienteRepository.ObterClientePorId(clienteId);
        if (cliente == null) throw new ArgumentException("Cliente não encontrado.");
    }
    
    private async Task<List<ProdutosPedido>> PreencherListaProdutosPedido( IEnumerable<InserirProdutosPedidoInputDto> produtos)
    {
        var produtosValidos = new List<ProdutosPedido>();

        foreach (var produto in produtos)
        {
            var produtoEntity = await produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            if (produtoEntity == null)
                throw new ArgumentException($"Produto {produto.ProdutoId} não encontrado.");

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
