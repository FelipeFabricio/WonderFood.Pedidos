using MediatR;
using Microsoft.AspNetCore.Mvc;
using WonderFood.Application.Pedidos.Commands.AlterarStatus;
using WonderFood.Models.Events;

namespace WonderFood.Worker.Webhooks;

[Route("/webhook/producao")]
public class ProducaoWebhook : ControllerBase
{
    private readonly ISender _mediator;

    public ProducaoWebhook(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Webhook que recebe a comunicação da alteração de status de um pedido.
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    [HttpPost("alteracao-status")]
    public async Task<IActionResult> AlterarStatusWebhook([FromBody] AlteracaoStatusEvent payload)
    {
        try
        {
            var command = new AlterarStatusPedidoCommand(payload);
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}