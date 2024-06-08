using Microsoft.EntityFrameworkCore;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.BDD.Tests.Common;

public sealed class InMemoryDbContextFactory : IDisposable
{
    private WonderFoodContext _context;

    public WonderFoodContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<WonderFoodContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new WonderFoodContext(options);
        _context.Database.EnsureCreated();
        SeedData(_context);
        return _context;
    }

    private void SeedData(WonderFoodContext context)
    {
        context.Clientes.Add(
            new Cliente(
                "João",
                "joao@email.com",
                "800.449.050-68",
                "1187876777",
                Guid.Parse("945f7aea-ae9e-43d0-9f8f-737c2a56f710"))
        );

        context.Produtos.AddRange(
            new Produto(
                "Pizza",
                10,
                CategoriaProduto.Lanche,
                "Lanche",
                Guid.Parse("e3547b5e-f97e-4e6b-a64b-54858baf7874")),
            new Produto(
                "Coca-Cola",
                5,
                CategoriaProduto.Bebida,
                "Bebida",
                Guid.Parse("45bfb226-96f2-4845-a55b-81206cdcf0ec"))
        );
        context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}