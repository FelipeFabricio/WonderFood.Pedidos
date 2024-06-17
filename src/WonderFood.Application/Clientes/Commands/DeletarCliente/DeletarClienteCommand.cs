using MediatR;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Commands.DeletarCliente;

public record DeletarClienteCommand(DeletarClienteInputDto DadosCliente) : IRequest<Unit>; 