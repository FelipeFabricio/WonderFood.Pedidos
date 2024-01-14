using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces.UseCases;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteUseCases _useCases;

    public ClienteController(IClienteUseCases useCases)
    {
        _useCases = useCases;
    }

    /// <summary>
    /// Obter todos os Clientes
    /// </summary>
    /// <response code="200">Retorna lista com todos os Clientes cadastrados</response>
    [HttpGet]
    public ActionResult<IEnumerable<ClienteOutputDto>> ObterTodosClientes()
    {
        try
        {
            return Ok(_useCases.ObterTodosClientes());
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }

    /// <summary>
    /// Obter um Cliente por Id
    /// </summary>
    /// <response code="200">Retorna dados do Cliente</response>
    [HttpGet]
    [Route("{id}")]
    public ActionResult<ClienteOutputDto> ObterClientePorId(Guid id)
    {
        try
        {
            return Ok(_useCases.ObterClientePorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }

    /// <summary>
    /// Cadastrar um novo Cliente
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public IActionResult InserirCliente([FromBody] InserirClienteInputDto cliente)
    {
        try
        {
            _useCases.InserirCliente(cliente);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }

    /// <summary>
    /// Atualiza os dados de um Cliente
    /// </summary>
    /// <response code="200"></response>
    [HttpPut]
    public IActionResult AtualizarCliente([FromBody] AtualizarClienteInputDto cliente)
    {
        try
        {
            _useCases.AtualizarCliente(cliente);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}