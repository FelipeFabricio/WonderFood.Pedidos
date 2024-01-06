using AutoMapper;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces;

namespace WonderFood.UseCases.Cliente;

public class ClienteUseCases : IClienteUseCases
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mappper;

    public ClienteUseCases(IClienteRepository repository, IMapper mappper)
    {
        _repository = repository;
        _mappper = mappper;
    }
    
    public IEnumerable<ClienteDto> ObterTodosClientes()
    {
        var clientes = _repository.ObterTodosClientes();
        return _mappper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public ClienteDto ObterClientePorId(Guid id)
    {
        var cliente = _repository.ObterClientePorId(id);
        return _mappper.Map<ClienteDto>(cliente);
    }

    public bool InserirCliente(InserirClienteInputDto cliente)
    {
        var clienteEntity = _mappper.Map<Core.Entities.Cliente>(cliente);
        return _repository.InserirCliente(clienteEntity);   
    }

    public bool AtualizarCliente(AtualizarClienteInputDto cliente)
    {
        var clienteEntity = _mappper.Map<Core.Entities.Cliente>(cliente);
        return _repository.AtualizarCliente(clienteEntity);
    }
}