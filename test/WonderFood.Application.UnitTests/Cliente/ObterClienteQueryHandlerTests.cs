using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WonderFood.Application.Clientes.Queries.ObterCliente;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.UnitTests.Cliente.Factory;
using Xunit;

namespace WonderFood.Application.UnitTests.Cliente;

public class ObterClienteQueryHandlerTests
{
    private readonly ObterClienteQueryHandler _sut;
    private readonly IClienteRepository _clienteRepository = Substitute.For<IClienteRepository>();
     
    public ObterClienteQueryHandlerTests()
    {
        _sut = new ObterClienteQueryHandler(_clienteRepository);
    }
    
    [Fact]
    [Trait("Application", "Clientes")]
    public async Task Handle_DeveRetornarCliente_QuandoClienteCadastradoNaBase()
    {
        // Arrange
        var query = ClientesFactory.CriarObterClienteQuery();
        var clienteEntity = ClientesFactory.CriarCliente();
        
        _clienteRepository.ObterClientePorId(Arg.Any<Guid>()).Returns(clienteEntity);
        
        // Act
        var result = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        await _clienteRepository.Received(1).ObterClientePorId(Arg.Any<Guid>());
        result.Id.Should().Be(clienteEntity.Id);
    }
    
    [Fact]
    [Trait("Application", "Clientes")]
    public async Task Handle_DeveLancarArgumentException_QuandoClienteNaoLocalizado()
    {
        // Arrange
        var query = ClientesFactory.CriarObterClienteQuery();
        _clienteRepository.ObterClientePorId(Arg.Any<Guid>()).ReturnsNull();
        
        // Act
        Func<Task> act = async () => await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Cliente não localizado");
    }
}