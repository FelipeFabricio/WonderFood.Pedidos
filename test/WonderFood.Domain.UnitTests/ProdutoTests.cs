using FluentAssertions;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.UnitTests;

public class ProdutoTests
{
    [Theory]
    [InlineData("Nome Produto", 1, CategoriaProduto.Acompanhamento, "descricao")]
    [InlineData("Nome Produto 2", 0.1, CategoriaProduto.Bebida, "descricao 2")]
    [InlineData("Nom", 1.5, CategoriaProduto.Lanche, "descricao 3")]
    public void Construtor_DeveCriarProduto_QuandoTodosOsDadosForemValidos(string nome, decimal valor, CategoriaProduto categoria, string descricao)
    {
        // Arrange e Act
        var cliente = new Produto(nome, valor, categoria, descricao);

        // Assert
        cliente.Nome.Should().Be(nome);
        cliente.Valor.Should().Be(valor);
        cliente.Categoria.Should().Be(categoria);
        cliente.Descricao.Should().Be(descricao);
        cliente.Id.Should().NotBe(Guid.Empty);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ValidarValor_DeveLancarArgumentException_QuandoValorMenorQueZero(decimal valor)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Produto("Nome Produto", 
            valor,
            CategoriaProduto.Acompanhamento, 
            "descricao"));
    }
    
    [Theory]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("ab")]
    public void ValidarNome_DeveLancarArgumentException_QuandoValorNomeForInvalido(string nome)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Produto(nome, 
            10M,
            CategoriaProduto.Acompanhamento, 
            "descricao"));
    }
}