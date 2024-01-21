using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Dtos;

public class AvancarStatusPedidoDto
{
    public StatusPedido Status { get; set; }
    public int NumeroPedido { get; set; }
}