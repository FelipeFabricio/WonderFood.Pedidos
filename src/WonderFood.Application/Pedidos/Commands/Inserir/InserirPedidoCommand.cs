using MediatR;
using WonderFood.Domain.Dtos.Pedido;

namespace WonderFood.Application.Pedidos.Commands.Inserir;

public record InserirPedidoCommand  (InserirPedidoInputDto Pedido) : IRequest<PedidosOutputDto>;