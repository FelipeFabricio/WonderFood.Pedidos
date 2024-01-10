using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.Infra.Sql.Repositories;

public class ProdutoRepository : IProdutoRepository
{  
    private readonly WonderFoodContext _context;

    public ProdutoRepository(WonderFoodContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> ObterTodosProdutos()
    {
        return _context.Produtos.ToList();
    }

    public IEnumerable<Produto> ObterProdutosPorCategoria(CategoriaProduto categoria)
    {
        return _context.Produtos.Where(x => x.Categoria == categoria).ToList();
    }

    public bool Inserir(Produto produto)
    {
        _context.Produtos.Add(produto);
        return _context.SaveChanges() > 0;
    }

}