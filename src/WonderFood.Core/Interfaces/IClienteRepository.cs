using WonderFood.Core.Entities;

namespace WonderFood.Core.Interfaces;

public interface IClienteRepository
{
    IEnumerable<Cliente> ObterTodosClientes();
    Cliente ObterClientePorId(Guid id);
    bool InserirCliente(Cliente cliente);
    bool AtualizarCliente(Cliente cliente);
}