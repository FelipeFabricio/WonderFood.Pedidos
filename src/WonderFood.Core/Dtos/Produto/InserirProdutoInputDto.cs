using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Dtos.Produto;

public class InserirProdutoInputDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public CategoriaProduto Categoria { get; set; }
}