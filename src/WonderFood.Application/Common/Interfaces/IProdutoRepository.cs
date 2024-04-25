using WonderFood.Domain.Entities;

namespace WonderFood.Application.Common.Interfaces;

public interface IProdutoRepository
{
    Task Inserir(Produto produto);
    Task<IEnumerable<Produto>> ObterTodosProdutos();
    Task<Produto?> ObterProdutoPorId(Guid id);
}