using WonderFood.Core.Dtos;

namespace WonderFood.Core.Interfaces.UseCases;

public interface IClienteUseCases
{
    IEnumerable<ClienteOutputDto> ObterTodosClientes();
    ClienteOutputDto ObterClientePorId(Guid id);
    void InserirCliente(InserirClienteInputDto cliente);
    void AtualizarCliente(AtualizarClienteInputDto cliente);
}