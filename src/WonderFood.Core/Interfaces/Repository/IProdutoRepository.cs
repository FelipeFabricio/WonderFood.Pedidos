using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces.Repository;

public interface IProdutoRepository
{
    void Inserir(Produto produto);
    IEnumerable<Produto> ObterTodosProdutos();
    Produto ObterProdutoPorId(Guid id);
    IEnumerable<Produto> ObterProdutosPorCategoria(CategoriaProduto categoria);
}