using WonderFood.Domain.Entities;

namespace WonderFood.Application.Common.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObterClientePorId(Guid id);
    Task InserirCliente(Cliente cliente);
}