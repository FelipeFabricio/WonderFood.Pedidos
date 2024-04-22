using MediatR;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Queries.ObterCliente;

public record ObterClienteQuery(Guid Id) : IRequest<ClienteOutputDto>;
