using MediatR;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.IniciarProducaoPedido;

public record ProcessarProducaoPedidoCommand(Guid IdPedido, SituacaoPagamento StatusPagamento) : IRequest<Unit>;
