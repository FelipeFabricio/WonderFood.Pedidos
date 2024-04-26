using Bogus;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities.Enums;
using Xunit;

namespace WonderFood.WebApi.UnitTests.Fixtures;


[CollectionDefinition(nameof(PedidoFixtureCollection))]
public class PedidoFixtureCollection : ICollectionFixture<PedidoFixture>
{
}


public class PedidoFixture
{
    
    public InserirPedidoInputDto GerarInserirPedidoInputDtoValido()
    {
        return new Faker<InserirPedidoInputDto>("pt_BR")
            .RuleFor(c => c.ClienteId, f => f.Random.Guid())
            .RuleFor(c => c.Observacao, f => f.Lorem.Sentence())
            .RuleFor(c => c.FormaPagamento, f => f.PickRandom<FormaPagamento>())
            .RuleFor(c => c.Produtos, _ => GerarLInserirProdutosPedidoInputDtoValido(3))
            .Generate();
    }
    
    public PedidosOutputDto GerarPedidosOutputDtoValido()
    {
        return new Faker<PedidosOutputDto>("pt_BR")
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.ClienteId, f => f.Random.Guid())
            .RuleFor(c => c.DataPedido, f => f.Date.Past())
            .RuleFor(c => c.Status, f => f.PickRandom<StatusPedido>().ToString())
            .RuleFor(c => c.ValorTotal, f => f.Random.Decimal(1, 100))
            .RuleFor(c => c.NumeroPedido, _ => 1)
            .RuleFor(c => c.Observacao, f => f.Lorem.Sentence())
            .RuleFor(c => c.FormaPagamento, f => f.PickRandom<FormaPagamento>())
            .RuleFor(c => c.Produtos, _ => GerarListaProdutosPedidoOutputDtoValido(3))
            .Generate();
    }
    
    public IEnumerable<ProdutosPedidoOutputDto> GerarListaProdutosPedidoOutputDtoValido(int quantidade)
    {
        return new Faker<ProdutosPedidoOutputDto>("pt_BR")

            .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
            .RuleFor(c => c.Valor, f => f.Random.Decimal(1, 100))
            .RuleFor(c => c.Quantidade, f => f.Random.Int(1, 100))
            .RuleFor(c => c.ProdutoId, f => f.Random.Guid())
            .RuleFor(c => c.Categoria, f => f.PickRandom<CategoriaProduto>().ToString())
            .Generate(quantidade);
    }
    
    public IEnumerable<InserirProdutosPedidoInputDto> GerarLInserirProdutosPedidoInputDtoValido(int quantidade)
    {
        return new Faker<InserirProdutosPedidoInputDto>("pt_BR")
            .RuleFor(c => c.ProdutoId, f => f.Random.Guid())
            .RuleFor(c => c.Quantidade, f => f.Random.Int(1, 100))
            .Generate(quantidade);
    }
}