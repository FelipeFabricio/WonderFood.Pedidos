using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WonderFood.Infra.Sql.Context
{
    public class WonderFoodContextFactory : IDesignTimeDbContextFactory<WonderFoodContext>
    {
        public WonderFoodContext CreateDbContext(string[] args)
        {
            //TODO: Refatorar essa classe.
            //Ela foi criada para resolver paliativamente o problema de não conseguir rodar o comando dotnet ef migrations add
            var optionsBuilder = new DbContextOptionsBuilder<WonderFoodContext>();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            optionsBuilder.UseMySql("Server=localhost;Database=wonderfood-db;Uid=userdb;Pwd=senhaForte123!;Connect Timeout=60;", serverVersion);

            return new WonderFoodContext(optionsBuilder.Options);
        }
    }
}
