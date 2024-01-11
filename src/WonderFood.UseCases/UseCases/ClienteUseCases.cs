using AutoMapper;
using WonderFood.Core.Dtos;
using WonderFood.Core.Interfaces.Repository;
using WonderFood.Core.Interfaces.UseCases;

namespace WonderFood.UseCases.UseCases;

public class ClienteUseCases : IClienteUseCases
{
    private readonly IClienteRepository _repository;
    private readonly IMapper _mappper;

    public ClienteUseCases(IClienteRepository repository, IMapper mappper)
    {
        _repository = repository;
        _mappper = mappper;
    }
    
    public IEnumerable<ClienteOutputDto> ObterTodosClientes()
    {
        var clientes = _repository.ObterTodosClientes();
        return _mappper.Map<IEnumerable<ClienteOutputDto>>(clientes);
    }

    public ClienteOutputDto ObterClientePorId(Guid id)
    {
        var cliente = _repository.ObterClientePorId(id);
        return _mappper.Map<ClienteOutputDto>(cliente);
    }

    public void InserirCliente(InserirClienteInputDto cliente)
    {
        var clienteEntity = _mappper.Map<Core.Entities.Cliente>(cliente);
        _repository.InserirCliente(clienteEntity);   
    }

    public void AtualizarCliente(AtualizarClienteInputDto cliente)
    {
        var clienteEntity = _mappper.Map<Core.Entities.Cliente>(cliente);
        _repository.AtualizarCliente(clienteEntity);
    }
}