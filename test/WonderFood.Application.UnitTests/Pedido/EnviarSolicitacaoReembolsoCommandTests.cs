using FluentAssertions;
using MassTransit;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.EnviarSolicitacaoReembolso;
using WonderFood.Application.UnitTests.Pedido.Factory;
using WonderFood.Models.Events;
using Xunit;

namespace WonderFood.Application.UnitTests.Pedido;

public class EnviarSolicitacaoReembolsoCommandTests
{
    private readonly IPedidoRepository _pedidoRepository = Substitute.For<IPedidoRepository>();
    private readonly IBus _sender = Substitute.For<IBus>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly EnviarSolicitacaoReembolsoCommandHandler _sut;
    
    public EnviarSolicitacaoReembolsoCommandTests()
    {
        _sut = new EnviarSolicitacaoReembolsoCommandHandler(_sender, _pedidoRepository, _unitOfWork);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveAlterarStatusPedido_EEnviarSolicitacaoReembolso_QuandoSolicitacaoReembolsoDoPedidoAprovado()
    {
        //Arrange
        var command = new EnviarSolicitacaoReembolsoCommand(Guid.NewGuid());
        var pedidoEntity = PedidosFactory.CriarPedidoEntitAguardandoPreparo();
        _pedidoRepository.ObterPorId(command.IdPedido).Returns(pedidoEntity);
        _pedidoRepository.AtualizarStatus(Arg.Any<Domain.Entities.Pedido>()).Returns(Task.CompletedTask);
        
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Unit>();
        await _sender.Received(1).Publish(Arg.Any<ReembolsoSolicitadoEvent>(), default);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveLancarExcecao_QuandoPedidoNaoEncontrado()
    {
        //Arrange
        var command = new EnviarSolicitacaoReembolsoCommand(Guid.NewGuid());
        _pedidoRepository.ObterPorId(command.IdPedido).ReturnsNull();
        
        //Act
        Func<Task> act = async () => await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Pedido não encontrado.");
    }
}