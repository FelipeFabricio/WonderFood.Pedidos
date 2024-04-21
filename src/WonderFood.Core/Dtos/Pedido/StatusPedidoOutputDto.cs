using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Dtos.Pedido;

public class StatusPedidoOutputDto
{
    public int NumeroPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
}