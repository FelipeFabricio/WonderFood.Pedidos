namespace WonderFood.Domain.Dtos.Produto;

public class ProdutosPedidoOutputDto
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
    public Guid ProdutoId { get; set; }
    public string Categoria { get; set; }
}