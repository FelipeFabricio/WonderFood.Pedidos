using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Application.Pedidos.Commands.ProcessarPagamento;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;
using WonderFood.Worker.Webhooks;

namespace WonderFood.Worker.UnitTests;

public class PagamentosWebhookTests
{
    private readonly PagamentosWebhook _sut;
    private readonly ISender _mediator = Substitute.For<ISender>();

    public PagamentosWebhookTests()
    {
        _sut = new PagamentosWebhook(_mediator);
    }
    
    [Fact]
    [Trait("Worker", "Pagamentos")]
    public async Task PagamentoProcessadoWebhook_DeveRetornarOk_QuandoProcessamentoForEfetuadoComSucesso()
    {
        //Arrange
        var payload = new PagamentoProcessadoEvent { IdPedido = Guid.NewGuid(), StatusPagamento = StatusPagamento.PagamentoAprovado };
        _mediator.Send(Arg.Any<ProcessarPagamentoPedidoCommand>()).Returns(Unit.Value);
    
        //Act
        var resultado = (OkResult)await _sut.PagamentoProcessadoWebhook(payload);
    
        //Assert
        resultado.StatusCode.Should().Be(200);
    }
    
    [Fact]
    [Trait("Worker", "Pagamentos")]
    public async Task PagamentoProcessadoWebhook_DeveRetornarBadRequest_QuandoForLancadaUmaException()
    {
        //Arrange
        var payload = new PagamentoProcessadoEvent { IdPedido = Guid.NewGuid(), StatusPagamento = StatusPagamento.PagamentoAprovado };
        _mediator.Send(Arg.Any<ProcessarPagamentoPedidoCommand>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.PagamentoProcessadoWebhook(payload);
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
}