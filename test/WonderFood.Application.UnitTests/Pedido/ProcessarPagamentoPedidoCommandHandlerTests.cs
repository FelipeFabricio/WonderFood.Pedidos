using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.EnviarParaProducao;
using WonderFood.Application.Pedidos.Commands.ProcessarPagamento;
using WonderFood.Application.UnitTests.Pedido.Factory;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;
using Xunit;

namespace WonderFood.Application.UnitTests.Pedido;

public class ProcessarPagamentoPedidoCommandHandlerTests
{
    private readonly ProcessarPagamentoPedidoCommandHandler _sut;
    private readonly IPedidoRepository _pedidoRepository = Substitute.For<IPedidoRepository>();
    private readonly ISender _sender = Substitute.For<ISender>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public ProcessarPagamentoPedidoCommandHandlerTests()
    {
        _sut = new ProcessarPagamentoPedidoCommandHandler(_pedidoRepository, _unitOfWork, _sender);
    }

    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveAlterarStatusPedido_EenviarParaProducao_QuandoPagamentoDoPedidoAprovado()
    {
        //Arrange
        var command = new ProcessarPagamentoPedidoCommand(Guid.NewGuid(), StatusPagamento.PagamentoAprovado);
        var pedidoEntity = PedidosFactory.CriarPedidoEntity();
        _pedidoRepository.ObterPorId(command.IdPedido).Returns(pedidoEntity);
        _pedidoRepository.AtualizarStatus(Arg.Any<Domain.Entities.Pedido>()).Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Unit>();
        await _sender.Received(1).Send(Arg.Any<EnviarPedidoProducaoCommand>(), default);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveAlterarStatusPedido_ENaoEnviarParaProducao_QuandoPagamentoDoPedidoRecusado()
    {
        //Arrange
        var command = new ProcessarPagamentoPedidoCommand(Guid.NewGuid(), StatusPagamento.PagamentoRecusado);
        var pedidoEntity = PedidosFactory.CriarPedidoEntity();
        _pedidoRepository.ObterPorId(command.IdPedido).Returns(pedidoEntity);
        _pedidoRepository.AtualizarStatus(Arg.Any<Domain.Entities.Pedido>()).Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Unit>();
        await _sender.Received(0).Send(Arg.Any<EnviarPedidoProducaoCommand>(), default);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveLancarArgumentException_QuandoPedidoNaoExistirNaBaseDeDados()
    {
        //Arrange
        var command = new ProcessarPagamentoPedidoCommand(Guid.NewGuid(), StatusPagamento.PagamentoRecusado);
        _pedidoRepository.ObterPorId(Arg.Any<Guid>()).ReturnsNull();
        
        //Act
        Func<Task> action = async () => await  _sut.Handle(command, CancellationToken.None);
        
        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }
}