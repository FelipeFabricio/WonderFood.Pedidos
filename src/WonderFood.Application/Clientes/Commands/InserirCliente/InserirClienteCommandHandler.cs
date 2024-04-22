using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Cliente;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Clientes.Commands.InserirCliente;

public class InserirClienteCommandHandler : IRequestHandler<InserirClienteCommand, ClienteOutputDto>    
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InserirClienteCommandHandler(IClienteRepository clienteRepository,  IUnitOfWork unitOfWork, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClienteOutputDto> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(request.Cliente.Nome, request.Cliente.Email, request.Cliente.Cpf);

        await _clienteRepository.InserirCliente(cliente);
        await  _unitOfWork.CommitChangesAsync();
        return _mapper.Map<ClienteOutputDto>(request.Cliente);
    }
}