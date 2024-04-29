using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.Clientes.Queries.ObterCliente;

public class ObterClienteQueryHandler(IClienteRepository clienteRepository)
    : IRequestHandler<ObterClienteQuery, ClienteOutputDto>
{
    public async Task<ClienteOutputDto> Handle(ObterClienteQuery request, CancellationToken cancellationToken)
    {
        var clienteEntity = await clienteRepository.ObterClientePorId(request.Id);
        
        if(clienteEntity is null)
            throw new ArgumentException("Cliente não localizado");
        
        return new ClienteOutputDto
        {
            Id = clienteEntity.Id,
            Nome = clienteEntity.Nome,
            Email = clienteEntity.Email,
            Cpf = clienteEntity.Cpf
        };
    }
}