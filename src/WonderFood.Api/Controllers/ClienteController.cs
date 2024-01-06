using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces;

namespace WonderFood.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteUseCases _useCases;

    public ClienteController(IClienteUseCases useCases)
    {
        _useCases = useCases;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClienteDto>> ObterTodosClientes()
    {
        var clientes = _useCases.ObterTodosClientes();
        return Ok(clientes);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<ClienteDto> ObterClientePorId(Guid id)
    {
        var cliente = _useCases.ObterClientePorId(id);
        return Ok(cliente);
    }

    [HttpPost]
    public ActionResult<bool> InserirCliente([FromBody] InserirClienteInputDto cliente)
    {
        var result = _useCases.InserirCliente(cliente);
        return Ok(result);
    }

    [HttpPut]
    public ActionResult<bool> AtualizarCliente([FromBody] AtualizarClienteInputDto cliente)
    {
        var result = _useCases.AtualizarCliente(cliente);
        return Ok(result);
    }
}