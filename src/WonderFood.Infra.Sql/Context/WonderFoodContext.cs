using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Sql.Context;

public class WonderFoodContext : DbContext
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
    
    public void SeedData()
    {
        var sqlScript = File.ReadAllText("../../src/WonderFood.Infra.Sql/Scripts/SeedData.sql");
        Database.ExecuteSqlRaw(sqlScript);
    }
}