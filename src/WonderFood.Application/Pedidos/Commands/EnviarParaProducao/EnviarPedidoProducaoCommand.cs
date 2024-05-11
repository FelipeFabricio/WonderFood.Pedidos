using MediatR;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Pedidos.Commands.EnviarParaProducao;

public record EnviarPedidoProducaoCommand(Pedido Pedido) : IRequest<Unit>;
