using MediatR;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Commands.DeletarCliente;

public class DeletarClienteCommand(DeletarClienteInputDto deletarCliente) : IRequest<Unit>; 