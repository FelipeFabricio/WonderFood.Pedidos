using Microsoft.EntityFrameworkCore;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.Infra.Sql.Pedidos;

public class PedidoRepository : IPedidoRepository
{
    private readonly WonderFoodContext _context;

    public PedidoRepository(WonderFoodContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObterPorNumeroPedido(int numeroPedido)
    {
        return await _context.Pedidos
            .Include(c => c.Cliente)
            .Include(p => p.Produtos)
                .ThenInclude(p => p.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.NumeroPedido == numeroPedido);
    }

    public Task<Pedido?> ObterPorId(Guid id)
    {
        return _context.Pedidos
            .Include(c => c.Cliente)
            .Include(p => p.Produtos)
                .ThenInclude(p => p.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task Inserir(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        return Task.CompletedTask;
    }

    public async Task Atualizar(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }
}