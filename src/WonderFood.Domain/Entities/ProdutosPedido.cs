namespace WonderFood.Domain.Entities;

public class ProdutosPedido
{
    public Guid PedidoId { get; set; }
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public Pedido Pedido { get; set; }
    public Produto Produto { get; set; }
}