using WonderFood.Core.Dtos.Cliente;

namespace WonderFood.Core.Interfaces.UseCases;

public interface IClienteUseCases
{
    ClienteOutputDto ObterClientePorId(Guid id);
    ClienteOutputDto InserirCliente(InserirClienteInputDto cliente);
}