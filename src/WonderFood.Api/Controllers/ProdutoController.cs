using Microsoft.AspNetCore.Mvc;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces;

namespace WonderFood.Api.Controllers;

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
        var produtos = _produtoUseCases.ObterTodosProdutos();
        return Ok(produtos);
    }
    
    /// <summary>
    /// Obter todos os Produtos de uma mesma Categoria
    /// </summary>
    /// <response code="200"></response>
    [HttpGet] 
    [Route("{categoria:int}")]
    public ActionResult<ProdutoOutputDto>  ObterProdutosPorCategoria(int categoria)
    {
        var produto = _produtoUseCases.ObterProdutoPorCategoria(categoria);
        return Ok(produto);
    }
    
    /// <summary>
    /// Cadastrar um novo Produto
    /// </summary>
    /// <response code="200"></response>
    [HttpPost]
    public ActionResult<bool> InserirProduto([FromBody] InserirProdutoInputDto produto)
    {
        _produtoUseCases.InserirProduto(produto);
        return Ok();
    }
}