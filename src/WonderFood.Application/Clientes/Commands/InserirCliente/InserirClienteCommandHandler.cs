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
        await _clienteRepository.InserirCliente(_mapper.Map<Cliente>(request.Cliente));
        await  _unitOfWork.CommitChangesAsync();
        return _mapper.Map<ClienteOutputDto>(request.Cliente);
    }
}