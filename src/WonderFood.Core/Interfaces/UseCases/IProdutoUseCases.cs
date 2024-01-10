using WonderFood.Core.Dtos;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces;

public interface IProdutoUseCases
{
    IEnumerable<ProdutoOutputDto> ObterTodosProdutos();
    IEnumerable<ProdutoOutputDto> ObterProdutoPorCategoria(int categoria);
    bool InserirProduto(InserirProdutoInputDto produto);
}