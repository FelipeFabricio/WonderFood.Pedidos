using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Queries.ObterCliente;

public class ObterClienteQueryHandler : IRequestHandler<ObterClienteQuery, ClienteOutputDto>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ObterClienteQueryHandler(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<ClienteOutputDto> Handle(ObterClienteQuery request, CancellationToken cancellationToken)
    {
        var clienteEntity = await _clienteRepository.ObterClientePorId(request.Id);
        return _mapper.Map<ClienteOutputDto>(clienteEntity);
    }
}