using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Models.Events;

public record StatusPedidoAlteradoEvent(int numeroPedido, StatusPedido status);