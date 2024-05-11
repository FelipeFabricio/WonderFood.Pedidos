using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.Inserir;

public class InserirPedidoCommandHandler(
    IPedidoRepository pedidoRepository,
    IUnitOfWork unitOfWork,
    IClienteRepository clienteRepository,
    IProdutoRepository produtoRepository,
    IWonderFoodPagamentoExternal pagamentosExternal,
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
        
        await EnviarSolicitacaoPagamento(pedido);

        return mapper.Map<PedidosOutputDto>(pedidoCadastrado);
    }

    private async Task EnviarSolicitacaoPagamento(Pedido pedido)
    {
        var pagamentoSolicitado = new PagamentoSolicitadoEvent
        {
            IdPedido = pedido.Id,
            ValorTotal = pedido.ValorTotal,
            FormaPagamento = (FormaPagamento)pedido.FormaPagamento,
            IdCliente = pedido.ClienteId,
            DataConfirmacaoPedido = DateTime.Now
        };
        await pagamentosExternal.EnviarSolicitacaoPagamento(pagamentoSolicitado);
    }

    private async Task ValidarCliente(Guid clienteId)
    {
        var cliente = await clienteRepository.ObterClientePorId(clienteId);
        if (cliente == null) throw new ArgumentException("Cliente não encontrado.");
    }
    
    private async Task<List<Domain.Entities.ProdutosPedido>> PreencherListaProdutosPedido( IEnumerable<InserirProdutosPedidoInputDto> produtos)
    {
        var produtosValidos = new List<Domain.Entities.ProdutosPedido>();

        foreach (var produto in produtos)
        {
            var produtoEntity = await produtoRepository.ObterProdutoPorId(produto.ProdutoId);
            if (produtoEntity == null)
                throw new ArgumentException($"Produto {produto.ProdutoId} não encontrado.");

            produtosValidos.Add(new Domain.Entities.ProdutosPedido
            {
                ProdutoId = produtoEntity.Id,
                Quantidade = produto.Quantidade,
                ValorProduto = produtoEntity.Valor
            });
        }
        return produtosValidos;
    }
}
