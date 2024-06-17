using WonderFood.Application.UnitTests.Produto.Factory;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Application.UnitTests.Pedido.Factory;

public static class PedidosFactory
{
    public static PedidosOutputDto CriarPedidoOutputDto()
    {
        return new PedidosOutputDto
        {
            Id = Guid.NewGuid(),
            ClienteId = Guid.NewGuid(),
            DataPedido = DateTime.Now,
            Status = "Aguardando Pagamento",
            ValorTotal = 100,
            NumeroPedido = 1,
            Observacao = "Observação",
            FormaPagamento = FormaPagamento.Dinheiro,
            Produtos = new List<ProdutosPedidoOutputDto>
            {
                new()
                {
                    ProdutoId = Guid.NewGuid(),
                    Quantidade = 1,
                }
            }
        };
    }

    public static Domain.Entities.Pedido CriarPedidoEntity()
    {
        return new Domain.Entities.Pedido(Guid.NewGuid(), CriarListaProdutosPedidoEntity(), FormaPagamento.Dinheiro);
    }
    
    public static IEnumerable<ProdutosPedido> CriarListaProdutosPedidoEntity()
    {
        return new List<ProdutosPedido>
        {
            new()
            {
                ProdutoId = Guid.NewGuid(),
                Quantidade = 1,
                PedidoId = Guid.NewGuid(),
                Produto = ProdutoFactory.CriarProdutoEntity()
            },
        };
    }

    public static Domain.Entities.Pedido CriarPedidoEntityComPagamentoAprovado()
    {
        var pedido = CriarPedidoEntity();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);
        return pedido;
    }
    
    public static Domain.Entities.Pedido CriarPedidoEntitAguardandoPreparo()
    {
        var pedido = CriarPedidoEntity();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);
        pedido.AlterarStatusPedido(StatusPedido.AguardandoPreparo);
        return pedido;
    }
}