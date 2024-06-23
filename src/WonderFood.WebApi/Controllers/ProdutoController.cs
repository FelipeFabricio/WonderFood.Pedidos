using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WonderFood.Application.Produtos.Commands.InserirProduto;
using WonderFood.Application.Produtos.Queries.ObterTodosProdutos;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ProdutoController(ISender mediator) : ControllerBase
{
    /// <summary>
    /// Obter todos os Produtos cadastrados
    /// </summary>
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Produtos</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProdutoOutputDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ObterTodosProdutos()
    {
        try
        {
            var response = await mediator.Send(new ObterTodosProdutosQuery());
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Cadastrar um novo Produto
    /// </summary>
    /// <response code="204">Cadastro com sucesso</response>
    /// <response code="400">Falha ao cadastrar Produtos</response>
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> InserirProduto([FromBody] InserirProdutoInputDto produto)
    {
        try
        {
            var command = new InserirProdutoCommand(produto);
            await mediator.Send(command);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}