using Microsoft.EntityFrameworkCore;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;

namespace WonderFood.Infra.Sql.Context;

public class WonderFoodContext : DbContext, IUnitOfWork
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ProdutosPedido> ProdutosPedido { get; set; }

    public WonderFoodContext(DbContextOptions<WonderFoodContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WonderFoodContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }
}