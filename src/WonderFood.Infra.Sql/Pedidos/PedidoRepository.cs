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

    public async Task<Pedido?> ObterPorId(Guid id)
    {
        return await _context.Pedidos
            .Include(c => c.Cliente)
            .Include(p => p.Produtos)
                .ThenInclude(p => p.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Inserir(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
    }

    public async Task AtualizarStatus(Pedido pedido)
    {
        await _context.Pedidos.Where(p => p.Id == pedido.Id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Status, pedido.Status));
    }
}