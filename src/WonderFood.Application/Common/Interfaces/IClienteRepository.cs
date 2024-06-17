using WonderFood.Domain.Entities;

namespace WonderFood.Application.Common.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObterClientePorId(Guid id);
    Task<Cliente?> ObterClientePorNumeroTelefone(string numeroTelefone);
    Task DeletarCliente(Guid id);
    Task InserirCliente(Cliente cliente);
}