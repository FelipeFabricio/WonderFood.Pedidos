using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Dtos;
using WonderFood.Core.Dtos.Cliente;
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
    /// Obter um Cliente por Id
    /// </summary>
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Cliente</response>
    [HttpGet("{id}")]
    public IActionResult ObterClientePorId(Guid id)
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
    /// <response code="201">Criado com sucesso</response>
    /// <response code="400">Falha ao cadastrar</response>
    [HttpPost]
    public IActionResult InserirCliente([FromBody] InserirClienteInputDto cliente)
    {
        try
        {
            var novoCliente = _useCases.InserirCliente(cliente);
            return CreatedAtAction(nameof(ObterClientePorId), new {id = novoCliente.Id}, novoCliente);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}