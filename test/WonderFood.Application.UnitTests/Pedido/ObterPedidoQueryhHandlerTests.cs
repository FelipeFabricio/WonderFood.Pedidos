using AutoMapper;
using FluentAssertions;
using NSubstitute;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Queries.ObterPedido;
using WonderFood.Application.UnitTests.Pedido.Factory;
using WonderFood.Domain.Dtos.Pedido;
using Xunit;

namespace WonderFood.Application.UnitTests.Pedido;

public class ObterPedidoQueryhHandlerTests
{
    private readonly ObterPedidoQueryhHandler _sut;
    private readonly IMapper _mapper = Substitute.For<IMapper>(); 
    private readonly IPedidoRepository _pedidoRepository = Substitute.For<IPedidoRepository>();

    public ObterPedidoQueryhHandlerTests()
    {
        _sut = new ObterPedidoQueryhHandler(_pedidoRepository, _mapper);
    }
    
    [Fact]
    [Trait("Application", "Pedidos")]
    public async Task Handle_DeveRetornarPedidoPorNumero_QuandoPedidoExistirNaBaseDeDados()
    {
        //Arrange
        var query = new ObterPedidoQuery(1);
        var pedidoEntity = PedidosFactory.CriarPedidoEntity();
        _pedidoRepository.ObterPorNumeroPedido(Arg.Any<int>()).Returns(pedidoEntity);
        _mapper.Map<PedidosOutputDto>(pedidoEntity).Returns(PedidosFactory.CriarPedidoOutputDto());
        
        //Act
        var result = await _sut.Handle(query, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<PedidosOutputDto>();
    }
}