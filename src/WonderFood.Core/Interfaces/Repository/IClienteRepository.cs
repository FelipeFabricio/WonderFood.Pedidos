using WonderFood.Core.Entities;

namespace WonderFood.Core.Interfaces.Repository;

public interface IClienteRepository
{
    IEnumerable<Cliente> ObterTodosClientes();
    Cliente ObterClientePorId(Guid id);
    void InserirCliente(Cliente cliente);
    void AtualizarCliente(Cliente cliente);
}