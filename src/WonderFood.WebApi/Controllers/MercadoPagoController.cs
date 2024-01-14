using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MercadoPagoController : ControllerBase
{
    private readonly IPedidoUseCases _pedidoUseCases;

    public MercadoPagoController(IPedidoUseCases pedidoUseCases)
    {
        _pedidoUseCases = pedidoUseCases;
    }
    
    /// <summary>
    /// Retorno Pagamento Mercado Pago - Mock
    /// </summary>
    /// <remarks>
    /// - Esse enpoint é utilizado para simular o retorno de pagamento via Mercado Pago, que será implementado futuramente como um Webhook.
    /// - Para confirmar um pagamento basta marcar o campo 'PagamentoAprovado' como true, e informar o número do pedido.
    /// </remarks>
    /// <response code="200">Recebido com sucesso</response>
    /// <response code="400">Recebido com falha</response>
    [HttpPost]
    public IActionResult RetornoPagamentoMercadoPago([FromBody] MercadoPagoRetornoMock retornoPagamento)
    {
        try
        {
            if(retornoPagamento.PagamentoAprovado)
                _pedidoUseCases.AtualizarStatusPedido(retornoPagamento.NumeroPedido, StatusPedido.Recebido);
            else
                _pedidoUseCases.AtualizarStatusPedido(retornoPagamento.NumeroPedido, StatusPedido.Cancelado);     
            
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}

public class MercadoPagoRetornoMock
{
    public int NumeroPedido { get; set; }
    public bool PagamentoAprovado { get; set; }
}