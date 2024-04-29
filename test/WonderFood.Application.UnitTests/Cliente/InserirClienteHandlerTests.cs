using FluentAssertions;
using MediatR;
using NSubstitute;
using WonderFood.Application.Clientes.Commands.InserirCliente;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.UnitTests.Cliente.Factory;

namespace WonderFood.Application.UnitTests.Cliente;

public class InserirClienteHandlerTests
{
    private readonly InserirClienteCommandHandler _sut;
    private readonly IClienteRepository _clienteRepository = Substitute.For<IClienteRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    
    public InserirClienteHandlerTests()
    {
        _sut = new InserirClienteCommandHandler(_clienteRepository, _unitOfWork);
    }

    [Fact]
    [Trait("Application", "Clientes")]
    public async Task Handle_DeveInserirCliente_QuandoDadosClienteValidos()
    {
        // Arrange
        var command = ClientesFactory.CriarInserirClienteCommand();
        _clienteRepository.InserirCliente(Arg.Any<Domain.Entities.Cliente>()).Returns(Task.CompletedTask);
        _unitOfWork.CommitChangesAsync().Returns(Task.CompletedTask);
        
        // Act
        var result = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        await _clienteRepository.Received(1).InserirCliente(Arg.Any<Domain.Entities.Cliente>());
        await _unitOfWork.Received(1).CommitChangesAsync();
        result.Should().Be(Unit.Value);
    }
}