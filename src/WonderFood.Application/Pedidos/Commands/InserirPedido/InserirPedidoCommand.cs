using MediatR;
using WonderFood.Domain.Dtos.Pedido;

namespace WonderFood.Application.Pedidos.Commands.InserirPedido;

public record InserirPedidoCommand  (InserirPedidoInputDto Pedido) : IRequest<Unit>;