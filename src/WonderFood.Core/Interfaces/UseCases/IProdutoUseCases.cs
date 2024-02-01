using WonderFood.Core.Dtos.Produto;

namespace WonderFood.Core.Interfaces.UseCases;

public interface IProdutoUseCases
{
    IEnumerable<ProdutoOutputDto> ObterTodosProdutos();
    IEnumerable<ProdutoOutputDto> ObterProdutoPorCategoria(int categoria);
    void InserirProduto(InserirProdutoInputDto produto);
}