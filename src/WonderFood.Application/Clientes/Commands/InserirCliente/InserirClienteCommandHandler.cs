using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Cliente;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Clientes.Commands.InserirCliente;

public class InserirClienteCommandHandler(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<InserirClienteCommand, ClienteOutputDto>
{
    public async Task<ClienteOutputDto> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(request.Cliente.Nome, request.Cliente.Email, 
            request.Cliente.Cpf, request.Cliente.NumeroTelefone);

        await clienteRepository.InserirCliente(cliente);
        await  unitOfWork.CommitChangesAsync();
        return new ClienteOutputDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Cpf = cliente.Cpf,
            NumeroTelefone = cliente.NumeroTelefone
        };
    }
}