using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public CategoriaProduto Categoria { get; set; }
}