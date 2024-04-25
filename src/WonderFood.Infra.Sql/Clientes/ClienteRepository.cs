using Microsoft.EntityFrameworkCore;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.Infra.Sql.Clientes;

public class ClienteRepository : IClienteRepository
{
    private readonly WonderFoodContext _context;
    
    public ClienteRepository(WonderFoodContext context)
    {
        _context = context;
    }
    
    public async Task<Cliente?> ObterClientePorId(Guid id)
    {
        return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task InserirCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        return Task.CompletedTask;
    }
}