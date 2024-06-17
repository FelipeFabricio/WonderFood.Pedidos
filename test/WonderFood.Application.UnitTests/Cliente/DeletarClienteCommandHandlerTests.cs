using FluentAssertions;
using MediatR;
using NSubstitute;
using WonderFood.Application.Clientes.Commands.DeletarCliente;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.UnitTests.Cliente.Factory;
using WonderFood.Domain.Dtos.Cliente;
using Xunit;

namespace WonderFood.Application.UnitTests.Cliente;

public class DeletarClienteCommandHandlerTests
{
    private readonly IClienteRepository _clienteRepository = Substitute.For<IClienteRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly DeletarClienteCommandHandler _sut;
    
    public DeletarClienteCommandHandlerTests()
    {
        _sut = new DeletarClienteCommandHandler(_clienteRepository, _unitOfWork);
    }
    
    [Fact]
    [Trait("Application", "Clientes")]
    public async Task Handle_DeveDeletarCliente_QuandoClienteExiste()
    {
        // Arrange
        var command = new DeletarClienteCommand(new DeletarClienteInputDto("Felipe", "(11) 99999-9999", "Rua Teste"));
        var cliente = ClientesFactory.CriarCliente();
        _clienteRepository.ObterClientePorNumeroTelefone(Arg.Any<string>()).Returns(cliente);
        
        // Act
        var result = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        await _clienteRepository.Received(1).DeletarCliente(cliente.Id);
        await _unitOfWork.Received(1).CommitChangesAsync();
        result.Should().Be(Unit.Value);
    }
    
    [Fact]
    [Trait("Application", "Clientes")]
    public void Handle_DeveLancarArgumentException_QuandoClienteNaoExiste()
    {
        // Arrange
        var command = new DeletarClienteCommand(new DeletarClienteInputDto("Felipe", "(11) 99999-9999", "Rua Teste"));
        _clienteRepository.ObterClientePorNumeroTelefone(Arg.Any<string>()).Returns((Domain.Entities.Cliente)null);
        
        // Act
        Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        act.Should().ThrowAsync<ArgumentException>().WithMessage("Cliente não encontrado");
    }
}