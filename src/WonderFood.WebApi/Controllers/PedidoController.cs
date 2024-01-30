using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Dtos;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces;

namespace WonderFood.WebApi.Controllers;

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
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Pedidos</response>
    [HttpGet]
    public IActionResult ObterPedidosEmAberto()
    {
        try
        {
            return Ok(_pedidoUseCases.ObterPedidosEmAberto());
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Obter o status atual de um Pedido
    /// </summary>
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Pedido</response>
    [HttpGet("{numeroPedido:int}")]
    public IActionResult ObterStatusPedido(int numeroPedido)
    {
        try
        {
            return Ok(_pedidoUseCases.ConsultarStatusPedido(numeroPedido));
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Cadastrar um novo Pedido
    /// </summary>
    /// <response code="201">Cadastrado com sucesso</response>
    /// <response code="400">Falha ao cadastrar Pedido</response>
    [HttpPost]
    public IActionResult InserirPedido([FromBody] InserirPedidoInputDto produto)
    {
        try
        {
            _pedidoUseCases.Inserir(produto);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Avançar o status de um Pedido
    /// </summary>
    /// <response code="201">Avanço status do Pedido com sucesso</response>
    /// <response code="400">Falha ao avançar status do Pedido</response>
    [HttpPut]
    public IActionResult AvançarPedido(int numeroPedido, StatusPedido novoStatus)
    {
        try
        {
            _pedidoUseCases.AtualizarStatusPedido(numeroPedido, novoStatus);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}