using WonderFood.Core.Entities;

namespace WonderFood.Core.Interfaces.Repository;

public interface IClienteRepository
{
    Cliente ObterClientePorId(Guid id);
    Cliente InserirCliente(Cliente cliente);
}