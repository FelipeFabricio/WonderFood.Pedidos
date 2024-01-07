using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Entities;

public class Pedido
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public DateTime DataPedido { get; set; }
    public StatusPedido Status { get; set; }
    public decimal ValorTotal { get; set; }
    public int NumeroPedido { get; set; }
    public string Observacao { get; set; }
    public IEnumerable<ProdutosPedido> Produtos { get; set; }
    public Cliente Cliente { get; set; }
}