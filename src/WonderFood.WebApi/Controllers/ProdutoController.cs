using MediatR;
using Microsoft.AspNetCore.Mvc;
using WonderFood.Application.Produtos.Commands.InserirProduto;
using WonderFood.Application.Produtos.Queries.ObterTodosProdutos;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly ISender _mediator;

    public ProdutoController(ISender mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// Obter todos os Produtos cadastrados
    /// </summary>
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Produtos</response>
    [HttpGet]
    public async Task<IActionResult> ObterTodosProdutos()
    {
        try
        {
            var response = await _mediator.Send(new ObterTodosProdutosQuery());
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
    /// <response code="201">Cadastro com sucesso</response>
    /// <response code="400">Falha ao cadastrar Produtos</response>
    [HttpPost]
    public async Task<ActionResult> InserirProduto([FromBody] InserirProdutoInputDto produto)
    {
        try
        {
            var command = new InserirProdutoCommand(produto);
            await _mediator.Send(command);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}