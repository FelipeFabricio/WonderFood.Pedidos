using MediatR;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Commands.DeletarCliente;

public record DeletarClienteCommandHandler : IRequestHandler<DeletarClienteCommand, Unit>
{
    public Task<Unit> Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}