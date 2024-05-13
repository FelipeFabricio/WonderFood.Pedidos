using Bogus;
using FluentAssertions;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.UnitTests;

public class PedidoTests
{
    [Theory]
    [InlineData(1, 10, FormaPagamento.Dinheiro, "Observacao")]
    [InlineData(3, 5, FormaPagamento.Pix, "obs")]
    [InlineData(4, 100.5, FormaPagamento.CartaoCredito, "Observacao 1")]
    [Trait("Domain", "Pedido")]
    public void Construtor_DeveCriarPedido_QuandoTodosOsDadosForemValidos(int quantidadeProdutos, decimal valorProduto,
        FormaPagamento formaPagamento, string observacao)
    {
        // Arrange
        var produtos = GerarListaProdutosPedido(valorProduto: valorProduto,
            quantidadeProdutos: quantidadeProdutos);

        //Act
        var pedido = new Pedido(Guid.NewGuid(), produtos, formaPagamento, observacao);

        // Assert
        pedido.ClienteId.Should().NotBe(Guid.Empty);
        pedido.FormaPagamento.Should().Be(formaPagamento);
        pedido.Observacao.Should().Be(observacao);
        pedido.Id.Should().NotBe(Guid.Empty);
        pedido.Status.Should().Be(StatusPedido.AguardandoPagamento);
        pedido.DataPedido.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(2));
        pedido.Produtos.Count().Should().Be(produtos.Count());
        pedido.ValorTotal.Should().Be(quantidadeProdutos * valorProduto);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [Trait("Domain", "Pedido")]
    public void PreencherListaProdutos_DeveLancarArgumentException_QuandoProdutosInvalidos(int quantidadeProdutos,
        int quantidadeItensLista)
    {
        // Arrange
        var listaProdutos = GerarListaProdutosPedido(10M, quantidadeItensLista, quantidadeProdutos);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Pedido(Guid.NewGuid(), listaProdutos, FormaPagamento.Dinheiro, "Observacao"));
    }

    [Theory]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoAguardandoPagamentoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoPagamentoAprovadoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoPagamentoRecusadoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoRecusado);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoAguardandoPreparoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);
        pedido.AlterarStatusPedido(StatusPedido.AguardandoPreparo);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }    
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoPreparoIniciadoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);
        pedido.AlterarStatusPedido(StatusPedido.AguardandoPreparo);
        pedido.AlterarStatusPedido(StatusPedido.PreparoIniciado);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoProntoParaRetiradaInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);
        pedido.AlterarStatusPedido(StatusPedido.AguardandoPreparo);
        pedido.AlterarStatusPedido(StatusPedido.PreparoIniciado);
        pedido.AlterarStatusPedido(StatusPedido.ProntoParaRetirada);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }    
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [InlineData(StatusPedido.Cancelado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoPedidoRetiradoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.PagamentoAprovado);
        pedido.AlterarStatusPedido(StatusPedido.AguardandoPreparo);
        pedido.AlterarStatusPedido(StatusPedido.PreparoIniciado);
        pedido.AlterarStatusPedido(StatusPedido.ProntoParaRetirada);
        pedido.AlterarStatusPedido(StatusPedido.PedidoRetirado);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(StatusPedido.AguardandoPagamento)]
    [InlineData(StatusPedido.PagamentoAprovado)]
    [InlineData(StatusPedido.PagamentoRecusado)]
    [InlineData(StatusPedido.AguardandoPreparo)]
    [InlineData(StatusPedido.PreparoIniciado)]
    [InlineData(StatusPedido.ProntoParaRetirada)]
    [InlineData(StatusPedido.PedidoRetirado)]
    [InlineData(StatusPedido.Cancelado)]
    [Trait("Domain", "Pedido")]
    public void AlterarStatusPedido_DeveLancarArgumentException_QuandoAlteracaoCanceladoInvalida(StatusPedido novoStatus)
    {
        // Arrange
        var pedido = GerarPedido();
        pedido.AlterarStatusPedido(StatusPedido.Cancelado);

        // Act & Assert
        pedido.Invoking(p => p.AlterarStatusPedido(novoStatus)).Should().Throw<ArgumentException>();
    }

    
    public IEnumerable<ProdutosPedido> GerarListaProdutosPedido(decimal valorProduto, 
        int quantidadeItensLista = 1,
        int quantidadeProdutos = 1)
    {
        return new Faker<ProdutosPedido>("pt_BR")
            .RuleFor(c => c.Quantidade, f => quantidadeProdutos)
            .RuleFor(c => c.ProdutoId, f => f.Random.Guid())
            .RuleFor(c => c.ValorProduto, valorProduto)
            .RuleFor(c => c.Produto, new Produto("Nome", 10M, CategoriaProduto.Acompanhamento, "Descricao"))
            .Generate(quantidadeItensLista);
    }

    public Pedido GerarPedido()
    {
        return new Pedido(Guid.NewGuid(), GerarListaProdutosPedido(10M, 1, 1), FormaPagamento.Dinheiro, "Observacao");
    }
}