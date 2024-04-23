using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Clientes.Commands.InserirCliente;

public class InserirClienteCommandHandler(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<InserirClienteCommand, Unit>
{
    public async Task<Unit> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(request.Cliente.Nome, request.Cliente.Email, request.Cliente.Cpf);

        await clienteRepository.InserirCliente(cliente);
        await  unitOfWork.CommitChangesAsync();
        return Unit.Value;
    }
}