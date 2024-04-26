using Bogus;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities.Enums;
using Xunit;

namespace WonderFood.WebApi.UnitTests.Fixtures;

[CollectionDefinition(nameof(ProdutoFixtureCollection))]
public class ProdutoFixtureCollection : ICollectionFixture<ProdutoFixture>
{
}

public class ProdutoFixture
{
    public IEnumerable<ProdutoOutputDto> GerarListaProdutoOutputDtoValido(int quantidade)
    {
        return new Faker<ProdutoOutputDto>("pt_BR")
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
            .RuleFor(c => c.Descricao, f => f.Commerce.ProductDescription())
            .RuleFor(c => c.Categoria, f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.Valor, f => f.Random.Decimal(1, 100))
            .Generate(quantidade);
    }
    
    public InserirProdutoInputDto GerarInserirProdutoInputDtoValido()
    {
        return new Faker<InserirProdutoInputDto>("pt_BR")
            .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
            .RuleFor(c => c.Descricao, f => f.Commerce.ProductDescription())
            .RuleFor(c => c.Categoria, f => f.PickRandom<CategoriaProduto>())
            .RuleFor(c => c.Valor, f => f.Random.Decimal(1, 100))
            .Generate();
    }
}