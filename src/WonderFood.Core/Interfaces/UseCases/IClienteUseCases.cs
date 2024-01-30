using WonderFood.Core.Dtos;

namespace WonderFood.Core.Interfaces.UseCases;

public interface IClienteUseCases
{
    IEnumerable<ClienteOutputDto> ObterTodosClientes();
    ClienteOutputDto ObterClientePorId(Guid id);
    ClienteOutputDto InserirCliente(InserirClienteInputDto cliente);
    ClienteOutputDto AtualizarCliente(AtualizarClienteInputDto cliente);
}