using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.AlterarStatus;
using WonderFood.Application.UnitTests.Pedido.Factory;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Events;
using Xunit;

namespace WonderFood.Application.UnitTests.Pedido;

public class AlterarStatusPedidoCommandHandlerTests
{
    private readonly AlterarStatusPedidoCommandHandler _sut;
    private readonly IPedidoRepository _pedidoRepository = Substitute.For<IPedidoRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public AlterarStatusPedidoCommandHandlerTests()
    {
        _sut = new AlterarStatusPedidoCommandHandler(_pedidoRepository, _unitOfWork);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveAtualizar_QuandoPedidoExistirNaBaseDeDados()
    {
        //Arrange
        var command = new AlterarStatusPedidoCommand(new AlteracaoStatusEvent
        {
            NumeroPedido = 1,
            Status = StatusPedido.PagamentoAprovado
        });
        
        var pedidoEntity = PedidosFactory.CriarPedidoEntity();
        _pedidoRepository.ObterPorNumeroPedido(Arg.Any<int>()).Returns(pedidoEntity);
        _pedidoRepository.AtualizarStatus(pedidoEntity).Returns(Task.CompletedTask);
        
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Unit>();
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveLancarArgumentException_QuandoPedidoNaoExistirNaBaseDeDados()
    {
        //Arrange
        var command = new AlterarStatusPedidoCommand(new AlteracaoStatusEvent
        {
            NumeroPedido = 1,
            Status = StatusPedido.PagamentoAprovado
        });

        _pedidoRepository.ObterPorNumeroPedido(Arg.Any<int>())!
            .Returns(Task.FromException<Domain.Entities.Pedido>(new ArgumentException("erro")));
        
        //Act
        Func<Task> action = async () => await  _sut.Handle(command, CancellationToken.None);
        
        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }
}