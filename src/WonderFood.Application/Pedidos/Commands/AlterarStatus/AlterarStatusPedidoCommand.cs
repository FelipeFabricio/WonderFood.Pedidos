using MediatR;
using WonderFood.Models.Events;

namespace WonderFood.Application.Pedidos.Commands.AlterarStatus;

public record AlterarStatusPedidoCommand(AlteracaoStatusEvent alteracaoStatus) :  IRequest<Unit>;