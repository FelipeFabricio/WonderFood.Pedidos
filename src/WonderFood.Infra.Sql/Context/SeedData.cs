using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Infra.Sql.Context;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new WonderFoodContext( 
                   serviceProvider.GetRequiredService<DbContextOptions<WonderFoodContext>>()))
        {
            if (context.Produtos.Any())
                return;
            
            context.Produtos.AddRange(
                new Produto("Hamburguer", 10.90m, CategoriaProduto.Lanche, "Top demais", Guid.Parse("e5d62425-d113-46ce-8769-58b07133d92b")));
            
            context.SaveChanges();
        }
    }
}