using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WonderFood.Infra.Data.Context;
using WonderFood.Infra.Data.ServiceExtensions;
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YourApiName v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
        }
    }
}
