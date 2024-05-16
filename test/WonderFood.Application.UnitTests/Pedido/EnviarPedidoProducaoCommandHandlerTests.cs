using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.EnviarParaProducao;
using WonderFood.Application.UnitTests.Pedido.Factory;
using WonderFood.Models.Events;
using Xunit;

namespace WonderFood.Application.UnitTests.Pedido;

public class EnviarPedidoProducaoCommandHandlerTests
{
    private readonly EnviarPedidoProducaoCommandHandler _sut;
    private readonly IPedidoRepository _pedidoRepository = Substitute.For<IPedidoRepository>();
    private readonly IWonderFoodProducaoExternal _wonderFoodProducaoExternal = Substitute.For<IWonderFoodProducaoExternal>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    public EnviarPedidoProducaoCommandHandlerTests()
    {
        _sut = new EnviarPedidoProducaoCommandHandler(_mapper, _pedidoRepository, _wonderFoodProducaoExternal, _unitOfWork);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveAlterarStatusPedido_EenviarParaProducao_QuandoPedidoExistirNaBaseDeDados()
    {
        //Arrange
        var command = new EnviarPedidoProducaoCommand(PedidosFactory.CriarPedidoEntityComPagamentoAprovado());
        _pedidoRepository.ObterPorId(command.Pedido.Id).Returns(command.Pedido);
        _pedidoRepository.AtualizarStatus(Arg.Any<Domain.Entities.Pedido>()).Returns(Task.CompletedTask);
        _wonderFoodProducaoExternal.EnviarParaProducao(Arg.Any<IniciarProducaoCommand>()).Returns(Task.CompletedTask);
        
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Unit>();
        await _wonderFoodProducaoExternal.Received(1).EnviarParaProducao(Arg.Any<IniciarProducaoCommand>());
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveLancarArgumentException_QuandoPedidoNaoExistirNaBaseDeDados()
    {
        //Arrange
        var command = new EnviarPedidoProducaoCommand(PedidosFactory.CriarPedidoEntityComPagamentoAprovado());

        _pedidoRepository.ObterPorId(Arg.Any<Guid>()).ReturnsNull();
        
        //Act
        Func<Task> action = async () => await  _sut.Handle(command, CancellationToken.None);
        
        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }
}