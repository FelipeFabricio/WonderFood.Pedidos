using Microsoft.EntityFrameworkCore;
using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.Infra.Sql.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly WonderFoodContext _context;

    public PedidoRepository(WonderFoodContext context)
    {
        _context = context;
    }

    public Pedido ObterPorNumeroPedido(int numeroPedido)
    {
        return _context.Pedidos.FirstOrDefault(p => p.NumeroPedido == numeroPedido);
    }

    public IEnumerable<Pedido> ObterPedidosEmAberto()
    {
        return _context.Pedidos.Where(p =>
                p.Status != StatusPedido.Cancelado &&
                p.Status != StatusPedido.Finalizado)
            .Include(p => p.Produtos)
                .ThenInclude(c => c.Produto)
            .OrderByDescending(p => p.Status)
            .ThenBy(p => p.DataPedido)
            .ToList();
    }

    public bool Inserir(Pedido pedido)
    {
        throw new NotImplementedException();
    }

    public bool AtualizarStatusPedido(Pedido pedido)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int numeroPedido)
    {
        throw new NotImplementedException();
    }
}