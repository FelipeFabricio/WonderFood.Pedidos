using MediatR;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Commands.InserirCliente;

public record InserirClienteCommand(InserirClienteInputDto Cliente) : IRequest<ClienteOutputDto>; 
