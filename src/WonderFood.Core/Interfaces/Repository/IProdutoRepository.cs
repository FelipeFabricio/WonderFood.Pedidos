using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces;

public interface IProdutoRepository
{
    bool Inserir(Produto produto);
    IEnumerable<Produto> ObterTodosProdutos();
    IEnumerable<Produto> ObterProdutosPorCategoria(CategoriaProduto categoria);
}