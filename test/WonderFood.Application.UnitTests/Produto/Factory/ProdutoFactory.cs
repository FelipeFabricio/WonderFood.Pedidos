using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Application.UnitTests.Produto.Factory;

public static class ProdutoFactory
{
    public static InserirProdutoInputDto CriarInserirProdutoInputDto(
        string nome = "Produto",
        decimal valor = 10.0m,
        string descricao = "Descrição do produto",
        CategoriaProduto categoria = CategoriaProduto.Bebida
        )
    {
        return new InserirProdutoInputDto()
        {
            Nome = nome,
            Valor = valor,
            Descricao = descricao,
            Categoria = categoria
        };
    }
    
    public static IEnumerable<Domain.Entities.Produto> CriarListaProdutosEntity()
    {
           return new List<Domain.Entities.Produto>
            {
                new("Lanche", 12.0m, CategoriaProduto.Lanche, "Descrição do Lanche"),
                new("Bebida", 10.0m, CategoriaProduto.Bebida, "Descrição da Bebida"),
                new("Sobremesa", 22.0m, CategoriaProduto.Sobremesa, "Descrição da Sobremesa")
            };
    }
    
    public static Domain.Entities.Produto CriarProdutoEntity(
        string nome = "Produto",
        decimal valor = 10.0m,
        string descricao = "Descrição do produto",
        CategoriaProduto categoria = CategoriaProduto.Bebida
        )
    {
        return new Domain.Entities.Produto(nome, valor,categoria, descricao);
    }

    public static IEnumerable<ProdutoOutputDto> CriarListaProdutosOutputDto()
    {
        return new List<ProdutoOutputDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Nome = "Lanche",
                Valor = 12.0m,
                Categoria = CategoriaProduto.Lanche.ToString(),
                Descricao = "Descrição do Lanche"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nome = "Bebida",
                Valor = 10.0m,
                Categoria = CategoriaProduto.Bebida.ToString(),
                Descricao = "Descrição da Bebida"
            }, 
            new()
            {
                Id = Guid.NewGuid(),
                Nome = "Sobremesa",
                Valor = 22.0m,
                Categoria = CategoriaProduto.Sobremesa.ToString(),
                Descricao = "Descrição da Sobremesa"
            }
        };
    }
}