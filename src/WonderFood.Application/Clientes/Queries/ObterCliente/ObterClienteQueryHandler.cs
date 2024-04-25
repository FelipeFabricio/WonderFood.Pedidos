using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Queries.ObterCliente;

public class ObterClienteQueryHandler(IClienteRepository clienteRepository, IMapper mapper)
    : IRequestHandler<ObterClienteQuery, ClienteOutputDto>
{
    public async Task<ClienteOutputDto> Handle(ObterClienteQuery request, CancellationToken cancellationToken)
    {
        var clienteEntity = await clienteRepository.ObterClientePorId(request.Id);
        return mapper.Map<ClienteOutputDto>(clienteEntity);
    }
}