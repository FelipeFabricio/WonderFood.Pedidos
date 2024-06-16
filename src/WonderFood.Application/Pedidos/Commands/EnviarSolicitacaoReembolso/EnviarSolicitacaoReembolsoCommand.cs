using MediatR;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Pedidos.Commands.EnviarSolicitacaoReembolso;

public record EnviarSolicitacaoReembolsoCommand(Guid IdPedido) : IRequest<Unit>;