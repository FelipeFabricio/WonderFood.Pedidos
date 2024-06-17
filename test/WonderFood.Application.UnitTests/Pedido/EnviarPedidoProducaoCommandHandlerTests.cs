using AutoMapper;
using FluentAssertions;
using MassTransit;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.EnviarParaProducao;
using WonderFood.Application.UnitTests.Pedido.Factory;
using Xunit;

namespace WonderFood.Application.UnitTests.Pedido;

public class EnviarPedidoProducaoCommandHandlerTests
{
    private readonly EnviarPedidoProducaoCommandHandler _sut;
    private readonly IPedidoRepository _pedidoRepository = Substitute.For<IPedidoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IBus _bus = Substitute.For<IBus>();

    public EnviarPedidoProducaoCommandHandlerTests()
    {
        _sut = new EnviarPedidoProducaoCommandHandler(_mapper, _pedidoRepository, _unitOfWork, _bus);
    }

    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveAlterarStatusPedido_EenviarParaProducao_QuandoPedidoExistirNaBaseDeDados()
    {
        //Arrange
        var command = new EnviarPedidoProducaoCommand(PedidosFactory.CriarPedidoEntityComPagamentoAprovado());
        _pedidoRepository.ObterPorId(command.Pedido.Id).Returns(command.Pedido);
        _pedidoRepository.AtualizarStatus(Arg.Any<Domain.Entities.Pedido>()).Returns(Task.CompletedTask);

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
        var command = new EnviarPedidoProducaoCommand(PedidosFactory.CriarPedidoEntityComPagamentoAprovado());

        _pedidoRepository.ObterPorId(Arg.Any<Guid>()).ReturnsNull();

        //Act
        Func<Task> action = async () => await _sut.Handle(command, CancellationToken.None);

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }
}