using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Dtos.Produto;

public class InserirProdutoInputDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public CategoriaProduto Categoria { get; set; }
}