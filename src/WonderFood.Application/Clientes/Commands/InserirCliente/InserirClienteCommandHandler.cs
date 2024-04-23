using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Clientes.Commands.InserirCliente;

public class InserirClienteCommandHandler : IRequestHandler<InserirClienteCommand, Unit>    
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InserirClienteCommandHandler(IClienteRepository clienteRepository,  IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(request.Cliente.Nome, request.Cliente.Email, request.Cliente.Cpf);

        await _clienteRepository.InserirCliente(cliente);
        await  _unitOfWork.CommitChangesAsync();
        return Unit.Value;
    }
}