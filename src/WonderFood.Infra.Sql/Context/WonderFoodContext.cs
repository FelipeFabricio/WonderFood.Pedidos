using Microsoft.EntityFrameworkCore;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Sagas;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Infra.Sql.Context;

public class WonderFoodContext : DbContext, IUnitOfWork
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ProdutosPedido> ProdutosPedido { get; set; }
    public DbSet<CriarPedidoSagaState> CriarPedidoSagaState { get; set; }

    public WonderFoodContext(DbContextOptions<WonderFoodContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WonderFoodContext).Assembly);
        modelBuilder.Entity<Produto>().HasData(new Produto("Hamburguer", 10.90m, CategoriaProduto.Lanche, "Top demais", Guid.Parse("e5d62425-d113-46ce-8769-58b07133d92b")));
        base.OnModelCreating(modelBuilder);
    }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }
}