using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WonderFood.Infra.Sql.Context;
using WonderFood.Infra.Sql.ServiceExtensions;
using WonderFood.UseCases.ServiceExtensions;

namespace WonderFood.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WonderFood", Version = "v1" });
            });
            services.AddUseCasesServices();
            services.AddInfraDataServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<WonderFoodContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WonderFoodContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WonderFood"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            dbContext.Database.Migrate();
            dbContext.SeedData();
        }
    }
}
