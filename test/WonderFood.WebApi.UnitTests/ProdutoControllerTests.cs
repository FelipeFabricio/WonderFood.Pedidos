using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Application.Produtos.Commands.InserirProduto;
using WonderFood.Application.Produtos.Queries.ObterTodosProdutos;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.WebApi.Controllers;
using WonderFood.WebApi.UnitTests.Fixtures;
using Xunit;

namespace WonderFood.WebApi.UnitTests;

[Collection(nameof(ProdutoFixtureCollection))]
public class ProdutoControllerTests
{
    private readonly ProdutoController _sut;
    private readonly ISender _mediator = Substitute.For<ISender>();
    private readonly ProdutoFixture _produtoFixture;
    
    public ProdutoControllerTests(ProdutoFixture produtoFixture)
    {
        _sut = new ProdutoController(_mediator);
        _produtoFixture = produtoFixture;
    }
    
    [Fact]
    [Trait("Controllers", "Produto")]
    public async Task ObterTodosProdutos_DeveRetornarListaProdutos_QuandoHouverProdutosCadastrados()
    {
        //Arrange
        var listaProdutos = _produtoFixture.GerarListaProdutoOutputDtoValido(2);
        _mediator.Send(Arg.Any<ObterTodosProdutosQuery>()).Returns(listaProdutos);
    
        //Act
        var resultado = (OkObjectResult)await _sut.ObterTodosProdutos();
    
        //Assert
        resultado.StatusCode.Should().Be(200);
        resultado.Value.As<IEnumerable<ProdutoOutputDto>>().Should().BeEquivalentTo(listaProdutos);
    }
    
    [Fact]
    [Trait("Controllers", "Produto")]
    public async Task ObterTodosProdutos_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        _mediator.Send(Arg.Any<ObterTodosProdutosQuery>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.ObterTodosProdutos();
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
    [Fact]
    [Trait("Controllers", "Produto")]
    public async Task InserirProduto_DeveRetornarNoContent_QuandoCadastroForEfetuado()
    {
        //Arrange
        var inserirProdutoRequest = _produtoFixture.GerarInserirProdutoInputDtoValido();
        _mediator.Send(Arg.Any<InserirProdutoCommand>()).Returns(Unit.Value);
    
        //Act
        var resultado = (NoContentResult)await _sut.InserirProduto(inserirProdutoRequest);
    
        //Assert
        resultado.StatusCode.Should().Be(204);
    }
    
    [Fact]
    [Trait("Controllers", "Produto")]
    public async Task InserirProduto_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        var inserirProdutoRequest = _produtoFixture.GerarInserirProdutoInputDtoValido();
        _mediator.Send(Arg.Any<InserirProdutoCommand>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.InserirProduto(inserirProdutoRequest);
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
}