using MediatR;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Pedidos.Commands.ProcessarPagamento;

public record ProcessarPagamentoPedidoCommand(Guid IdPedido, StatusPagamento StatusPagamento) : IRequest<Unit>;
