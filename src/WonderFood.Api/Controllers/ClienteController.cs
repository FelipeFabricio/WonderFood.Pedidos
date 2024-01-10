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

    /// <summary>
    /// Obter todos os Clientes
    /// </summary>
    /// <response code="200">Retorna lista com todos os Clientes cadastrados</response>
    [HttpGet]
    public ActionResult<IEnumerable<ClienteOutputDto>> ObterTodosClientes()
    {
        var clientes = _useCases.ObterTodosClientes();
        return Ok(clientes);
    }

    /// <summary>
    /// Obter um Cliente por Id
    /// </summary>
    /// <response code="200">Retorna dados do Cliente</response>
    [HttpGet]
    [Route("{id}")]
    public ActionResult<ClienteOutputDto> ObterClientePorId(Guid id)
    {
        var cliente = _useCases.ObterClientePorId(id);
        return Ok(cliente);
    }

    /// <summary>
    /// Cadastrar um novo Cliente
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public ActionResult<bool> InserirCliente([FromBody] InserirClienteInputDto cliente)
    {
        var result = _useCases.InserirCliente(cliente);
        return Ok(result);
    }

    /// <summary>
    /// Atualiza os dados de um Cliente
    /// </summary>
    /// <response code="200"></response>
    [HttpPut]
    public ActionResult<bool> AtualizarCliente([FromBody] AtualizarClienteInputDto cliente)
    {
        var result = _useCases.AtualizarCliente(cliente);
        return Ok(result);
    }
}