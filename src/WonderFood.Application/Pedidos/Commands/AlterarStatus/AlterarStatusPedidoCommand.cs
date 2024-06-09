using MediatR;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.AlterarStatus;

public record AlterarStatusPedidoCommand(StatusPedidoAlteradoEvent alteracaoStatus) :  IRequest<Unit>;