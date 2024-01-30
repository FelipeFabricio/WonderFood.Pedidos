using WonderFood.Core.Entities;

namespace WonderFood.Core.Interfaces.Repository;

public interface IClienteRepository
{
    IEnumerable<Cliente> ObterTodosClientes();
    Cliente ObterClientePorId(Guid id);
    Cliente InserirCliente(Cliente cliente);
    Cliente AtualizarCliente(Cliente cliente);
}