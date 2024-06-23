using MediatR;
using Microsoft.AspNetCore.Mvc;
using WonderFood.Application.Clientes.Commands.DeletarCliente;
using WonderFood.Application.Clientes.Commands.InserirCliente;
using WonderFood.Application.Clientes.Queries.ObterCliente;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ClienteController(ISender mediator) : ControllerBase
{
    /// <summary>
    /// Obter um Cliente por Id
    /// </summary>
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Cliente</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClienteOutputDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ObterClientePorId(Guid id)
    {
        try
        {
            var command = new ObterClienteQuery(id);
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }

    /// <summary>
    /// Cadastrar um novo Cliente
    /// </summary>
    /// <response code="200">Criado com sucesso</response>
    /// <response code="400">Falha ao cadastrar</response>
    [HttpPost]
    [ProducesResponseType(typeof(ClienteOutputDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> InserirCliente([FromBody] InserirClienteInputDto cliente)
    {
        try
        {
            var command = new InserirClienteCommand(cliente);
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Remover dados do Cliente
    /// </summary>
    /// <response code="204">Deletado com sucesso</response>
    /// <response code="400">Falha ao deletar</response>
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeletarCliente([FromQuery] string nome, string numeroCelular, string endereco)
    {
        try
        {
            var command = new DeletarClienteCommand(new DeletarClienteInputDto(nome, numeroCelular, endereco));
            await mediator.Send(command);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}