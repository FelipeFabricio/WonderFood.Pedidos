using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Application.Pedidos.Commands.InserirPedido;
using WonderFood.Application.Pedidos.Queries.ObterPedido;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.WebApi.Controllers;
using WonderFood.WebApi.UnitTests.Fixtures;
using Xunit;

namespace WonderFood.WebApi.UnitTests;

[Collection(nameof(PedidoFixtureCollection))]
public class PedidoControllerTests
{
    private readonly PedidoFixture _pedidoFixture;
    private readonly PedidoController _sut;
    private readonly ISender _mediator = Substitute.For<ISender>();
    
    public PedidoControllerTests(PedidoFixture pedidoFixture)
    {
        _sut = new PedidoController(_mediator);
        _pedidoFixture = pedidoFixture;
    }
    
    [Fact]
    [Trait("Controllers", "Pedido")]
    public async Task ObterPedido_DeveRetornarPedido_QuandoHouverPedidoCadastrado()
    {
        //Arrange
        var pedidoResponse = _pedidoFixture.GerarPedidosOutputDtoValido();
        _mediator.Send(Arg.Any<ObterPedidoQuery>()).Returns(pedidoResponse);
    
        //Act
        var resultado = (OkObjectResult)await _sut.ObterPedido(1);
    
        //Assert
        resultado.StatusCode.Should().Be(200);
        resultado.Value.As<PedidosOutputDto>().Should().BeEquivalentTo(pedidoResponse);
    }
    
    [Fact]
    [Trait("Controllers", "Pedido")]
    public async Task ObterPedido_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        _mediator.Send(Arg.Any<ObterPedidoQuery>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.ObterPedido(1);
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
    [Fact]
    [Trait("Controllers", "Pedido")]
    public async Task InserirPedido_DeveRetornarNoContent_QuandoCadastroPedidoForRealizado()
    {
        //Arrange
        var pedidoRequest = _pedidoFixture.GerarInserirPedidoInputDtoValido();
        _mediator.Send(Arg.Any<InserirPedidoCommand>()).Returns(Unit.Value);
    
        //Act
        var resultado = (NoContentResult)await _sut.InserirPedido(pedidoRequest);
    
        //Assert
        resultado.StatusCode.Should().Be(204);
    }
    
    [Fact]
    [Trait("Controllers", "Pedido")]
    public async Task InserirPedido_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        var pedidoRequest = _pedidoFixture.GerarInserirPedidoInputDtoValido();
        _mediator.Send(Arg.Any<InserirPedidoCommand>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.InserirPedido(pedidoRequest);
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
}