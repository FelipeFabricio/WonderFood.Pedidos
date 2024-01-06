using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WonderFood.Infra.Data.Context
{
    public class WonderFoodContextFactory : IDesignTimeDbContextFactory<WonderFoodContext>
    {
        public WonderFoodContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WonderFoodContext>();
            var config = new ConfigurationBuilder()
                    //.SetBasePath("C:\\WonderFood\\WonderFood.sln")
                    //.AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=master;User Id=sa;Password=admin123!;TrustServerCertificate=True");

            return new WonderFoodContext(optionsBuilder.Options);
        }
    }
}
