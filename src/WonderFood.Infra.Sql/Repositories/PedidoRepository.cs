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

    public void Inserir(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();
    }
}