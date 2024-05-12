using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Application.Pedidos.Commands.AlterarStatus;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Events;
using WonderFood.Worker.Webhooks;

namespace WonderFood.Worker.UnitTests;

public class ProducaoWebhookTests
{
    private readonly ProducaoWebhook _sut;
    private readonly ISender _mediator = Substitute.For<ISender>();
    
    public ProducaoWebhookTests()
    {
        _sut = new ProducaoWebhook(_mediator);
    }
      
    [Fact]
    [Trait("Worker", "Producao")]
    public async Task AlterarStatusWebhook_DeveRetornarOk_QuandoProcessamentoForEfetuadoComSucesso()
    {
        //Arrange
        var payload = new AlteracaoStatusEvent { NumeroPedido = 1, Status = StatusPedido.PreparoIniciado };
        _mediator.Send(Arg.Any<AlterarStatusPedidoCommand>()).Returns(Unit.Value);
    
        //Act
        var resultado = (OkResult)await _sut.AlterarStatusWebhook(payload);
    
        //Assert
        resultado.StatusCode.Should().Be(200);
    }
    
    [Fact]
    [Trait("Worker", "Producao")]
    public async Task AlterarStatusWebhook_DeveRetornarBadRequest_QuandoForLancadaUmaException()
    {
        //Arrange
        var payload = new AlteracaoStatusEvent { NumeroPedido = 1, Status = StatusPedido.PreparoIniciado };
        _mediator.Send(Arg.Any<AlterarStatusPedidoCommand>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.AlterarStatusWebhook(payload);
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
}