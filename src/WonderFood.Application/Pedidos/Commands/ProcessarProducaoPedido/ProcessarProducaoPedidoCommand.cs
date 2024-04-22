using MediatR;
using Wonderfood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.IniciarProducaoPedido;

public record ProcessarProducaoPedidoCommand(Guid IdPedido, SituacaoPagamento StatusPagamento) : IRequest<Unit>;
