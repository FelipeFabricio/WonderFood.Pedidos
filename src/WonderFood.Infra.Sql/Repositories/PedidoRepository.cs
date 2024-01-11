using Microsoft.EntityFrameworkCore;
using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces;
using WonderFood.Core.Interfaces.Repository;
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

    public void Inserir(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();
    }

    public void AtualizarStatusPedido(int numeroPedido, StatusPedido novoStatus)
    {
        var pedido = ObterPorNumeroPedido(numeroPedido);
        if (pedido is null) throw new Exception("Pedido não encontrado");
        
        pedido.Status = novoStatus;
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }

    public void Delete(int numeroPedido)
    {
        var pedido = ObterPorNumeroPedido(numeroPedido);
        if (pedido is null) throw new Exception("Pedido não encontrado");
        _context.Pedidos.Remove(pedido);
        _context.SaveChanges();
    }
}