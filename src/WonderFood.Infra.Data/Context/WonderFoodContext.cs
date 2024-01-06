using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WonderFood.Core.Entities;

namespace WonderFood.Infra.Data.Context;

public class WonderFoodContext : DbContext
{
    public WonderFoodContext(DbContextOptions<WonderFoodContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WonderFoodContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}