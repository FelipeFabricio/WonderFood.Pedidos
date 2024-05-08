using MediatR;
using Microsoft.AspNetCore.Mvc;
using WonderFood.Application.Pedidos.Commands.ProcessarProducaoPedido;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Controllers;

[Route("/webhook/pagamentos")]
public class PagamentosWebhook : ControllerBase
{
    private readonly ISender _mediator;

    public PagamentosWebhook(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Webhook que recebe o retorno do processamento de pagamento de um pedido.
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    [HttpPost("pagamento-processado")]
    public async Task<IActionResult> PagamentoProcessadoWebhook([FromBody] PagamentoProcessadoEvent payload)
    {
        try
        {
            var command = new ProcessarProducaoPedidoCommand(payload.IdPedido, payload.StatusPagamento);
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(400, new { e.Message});
        }
    }
}
