using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces.UseCases;
using WonderFood.WebApi.Controllers;
using WonderFood.WebApi.UnitTests.Fixtures;
using Xunit;

namespace WonderFood.WebApi.UnitTests;

[Collection(nameof(ClienteFixtureCollection))]
public class ClienteControllerTests
{
    private readonly ClienteController _sut;
    private readonly IClienteUseCases _clienteUseCases = Substitute.For<IClienteUseCases>();
    private readonly ClienteFixture _clienteFixture;

    public ClienteControllerTests(ClienteFixture clienteFixture)
    {
        _sut = new ClienteController(_clienteUseCases);
        _clienteFixture = clienteFixture;
    }

    [Fact]
    [Trait("Controllers", "Cliente")]
    public void ObterTodosClientes_DeveRetornarTodosClientesEStatusOK_QuandoHouverClientesCadastrados()
    {
        //Arrange
        var clientesCadastrados = _clienteFixture.GerarListaClienteOutputDtoValido();
        _clienteUseCases.ObterTodosClientes().Returns(clientesCadastrados);

        //Act
        var resultado = (OkObjectResult)_sut.ObterTodosClientes();

        //Assert
        resultado.StatusCode.Should().Be(200);
        resultado.Value.As<IEnumerable<ClienteOutputDto>>().Should().BeEquivalentTo(clientesCadastrados);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void ObterTodosClientes_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        _clienteUseCases.ObterTodosClientes().Throws(new Exception());

        //Act
        var resultado = (BadRequestObjectResult)_sut.ObterTodosClientes();

        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void ObterClientePorId_DeveRetornarClienteEStatusOK_QuandoHouverClienteCadastrado()
    {
        //Arrange
        var cliente = _clienteFixture.GerarClienteOutputDtoValido();
        var clienteId = cliente.Id;
        _clienteUseCases.ObterClientePorId(clienteId).Returns(cliente);

        //Act
        var resultado = (OkObjectResult)_sut.ObterClientePorId(clienteId);

        //Assert
        resultado.StatusCode.Should().Be(200);
        resultado.Value.As<ClienteOutputDto>().Should().BeEquivalentTo(cliente);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void ObterClientePorId_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        _clienteUseCases.ObterClientePorId(Arg.Any<Guid>()).Throws(new Exception());

        //Act
        var resultado = (BadRequestObjectResult)_sut.ObterClientePorId(Guid.NewGuid());

        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void InserirCliente_DeveRetornarClienteCadastradoEStatusCreated_QuandoClienteCadastrado()
    {
        //Arrange
        var clienteInput = _clienteFixture.GerarInserirClienteInputDtoValido();
        var clienteOutput = _clienteFixture.GerarClienteOutputDtoValido();
        _clienteUseCases.InserirCliente(clienteInput).Returns(clienteOutput);

        //Act
        var resultado = (CreatedAtActionResult)_sut.InserirCliente(clienteInput);

        //Assert
        resultado.StatusCode.Should().Be(201);
        resultado.Value.As<ClienteOutputDto>().Should().BeEquivalentTo(clienteOutput);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void InserirCliente_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        var clienteInput = _clienteFixture.GerarInserirClienteInputDtoValido();
        _clienteUseCases.InserirCliente(clienteInput).Throws(new Exception());

        //Act
        var resultado = (BadRequestObjectResult)_sut.InserirCliente(clienteInput);

        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void AtualizarCliente_DeveRetornarClienteEStatusOk_QuandoClienteAtualizado()
    {
        //Arrange
        var clienteInput = _clienteFixture.GerarAtualizarClienteInputDtoValido();
        var clienteOutput = _clienteFixture.GerarClienteOutputDtoValido();
        _clienteUseCases.AtualizarCliente(clienteInput).Returns(clienteOutput);
        
        //Act
        var resultado = (OkObjectResult)_sut.AtualizarCliente(clienteInput);

        //Assert
        resultado.StatusCode.Should().Be(200);
        resultado.Value.As<ClienteOutputDto>().Should().BeEquivalentTo(clienteOutput);
    }
    
    [Fact]
    [Trait("Controllers", "Cliente")]
    public void AtualizarCliente_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        var clienteInput = _clienteFixture.GerarAtualizarClienteInputDtoValido();
        _clienteUseCases.AtualizarCliente(clienteInput).Throws(new Exception());

        //Act
        var resultado = (BadRequestObjectResult)_sut.AtualizarCliente(clienteInput);

        //Assert
        resultado.StatusCode.Should().Be(400);
    }
}