using WonderFood.Core.Dtos;

namespace WonderFood.Core.Interfaces;

public interface IClienteUseCases
{
    public IEnumerable<ClienteDto> ObterTodosClientes();
    public ClienteDto ObterClientePorId(Guid id);
    public bool InserirCliente(InserirClienteInputDto cliente);
    public bool AtualizarCliente(AtualizarClienteInputDto cliente);
}