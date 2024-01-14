using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces;
using WonderFood.Core.Interfaces.UseCases;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoUseCases _produtoUseCases;
    
    public ProdutoController(IProdutoUseCases produtoUseCases)
    {
        _produtoUseCases = produtoUseCases;
    }
    
    /// <summary>
    /// Obter todos os Produtos cadastrados
    /// </summary>
    /// <response code="200"></response>
    [HttpGet]
    public IActionResult ObterTodosProdutos()
    {
        try
        {
            return Ok(_produtoUseCases.ObterTodosProdutos());
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Obter todos os Produtos de uma mesma Categoria
    /// </summary>
    /// <response code="200"></response>
    [HttpGet] 
    [Route("{categoria:int}")]
    public ActionResult<ProdutoOutputDto>  ObterProdutosPorCategoria(int categoria)
    {
        try
        {
            return Ok(_produtoUseCases.ObterProdutoPorCategoria(categoria));
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Cadastrar um novo Produto
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public ActionResult InserirProduto([FromBody] InserirProdutoInputDto produto)
    {
        try
        {
            _produtoUseCases.InserirProduto(produto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}