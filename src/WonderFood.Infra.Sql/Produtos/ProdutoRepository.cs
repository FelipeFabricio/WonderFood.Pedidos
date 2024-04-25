using Microsoft.EntityFrameworkCore;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.Infra.Sql.Produtos;

public class ProdutoRepository : IProdutoRepository
{
    private readonly WonderFoodContext _context;

    public ProdutoRepository(WonderFoodContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ObterTodosProdutos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto?> ObterProdutoPorId(Guid id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task Inserir(Produto produto)
    {
        _context.Produtos.Add(produto);
        return Task.CompletedTask;
    }
}