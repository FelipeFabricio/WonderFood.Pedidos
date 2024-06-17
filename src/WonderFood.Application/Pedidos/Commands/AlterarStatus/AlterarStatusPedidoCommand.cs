using MediatR;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.AlterarStatus;

public record AlterarStatusPedidoCommand(Guid IdPedido, StatusPedido NovoStatusPedido) :  IRequest<Unit>;