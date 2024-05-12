using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Models.Events;

public class AlteracaoStatusEvent
{
    public int NumeroPedido { get; set; }
    public StatusPedido Status { get; set; }
}