using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using Serilog;
using WonderFood.Infra.Sql.Context;
using WonderFood.Infra.Sql.ServiceExtensions;
using WonderFood.UseCases.ServiceExtensions;

namespace WonderFood.WebApi
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

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddUseCasesServices();
            services.AddInfraDataServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? 
                                   Configuration["ConnectionString"];

            services.AddDbContext<WonderFoodContext>(options => options.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WonderFoodContext dbContext)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WonderFood"));
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            ExecuteDatabaseMigration(dbContext);
        }

        private void ExecuteDatabaseMigration(WonderFoodContext dbContext)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(new[]
                    {
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(4),
                        TimeSpan.FromSeconds(8),
                        TimeSpan.FromSeconds(16),
                        TimeSpan.FromSeconds(32),
                    },
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Log.Logger.Information(
                            $"Tentativa {retryCount} de conexão ao SQL Server falhou. Tentando novamente em {timeSpan.Seconds} segundos.");
                    });

            retryPolicy.Execute(() =>
            {
                dbContext.Database.Migrate();
            });
        }
    }
}