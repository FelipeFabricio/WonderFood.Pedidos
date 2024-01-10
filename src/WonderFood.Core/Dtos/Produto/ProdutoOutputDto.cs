using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Dtos;

public class ProdutoOutputDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public string Categoria { get; set; }
}