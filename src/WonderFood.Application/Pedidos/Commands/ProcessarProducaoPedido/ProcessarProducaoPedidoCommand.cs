using MediatR;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarProducaoPedido;

public record ProcessarProducaoPedidoCommand(Guid IdPedido, StatusPagamento StatusPagamento) : IRequest<Unit>;
