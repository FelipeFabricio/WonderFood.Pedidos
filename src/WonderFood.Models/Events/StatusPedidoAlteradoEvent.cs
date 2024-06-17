using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Models.Events;

public class StatusPedidoAlteradoEvent
{
    public Guid PedidoId { get; set; }
    public StatusPedido Status { get; set; }
    public string MotivoCancelamento { get; set; }
};