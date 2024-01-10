using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Interfaces;

namespace WonderFood.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController  : ControllerBase
{
    private readonly IPedidoUseCases _pedidoUseCases;
    
    public PedidoController(IPedidoUseCases pedidoUseCases)
    {
        _pedidoUseCases = pedidoUseCases;
    }
    
    /// <summary>
    /// Obter todos os Pedidos que estão em aberto
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public IActionResult ObterPedidosEmAberto()
    {
        var pedidos = _pedidoUseCases.ObterPedidosEmAberto();
        return Ok(pedidos);
    }
    
    /// <summary>
    /// Obter o status atual de um Pedido
    /// </summary>
    /// <response code="200"></response>
    [HttpGet("{numeroPedido:int}")]
    public IActionResult ObterStatusPedido(int numeroPedido)
    {
        var statusPagamento = _pedidoUseCases.ConsultarStatusPedido(numeroPedido);
        return Ok(statusPagamento);
    }
    
}