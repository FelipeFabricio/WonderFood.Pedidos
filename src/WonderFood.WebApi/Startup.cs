using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
using WonderFood.Application;
using WonderFood.ExternalServices;
using WonderFood.Infra.Sql;
using WonderFood.Infra.Sql.Context;

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
           services.Configure<ExternalServicesSettings>(Configuration.GetSection("ExternalServicesSettings"));
           services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
               });

           services.AddEndpointsApiExplorer();
           services.AddApplication();
           services.AddExternalServices();
           services.AddSqlInfrastructure(Configuration);
           services.AddSwagger();
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
       }
       
       public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WonderFoodContext dbContext)
       {
           app.UseSwaggerMiddleware();
           app.UseRouting();
           app.UseAuthorization();
           app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();
           });
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
                   (_, timeSpan, retryCount, _) =>
                   {
                       Log.Logger.Error("Tentativa {0} de conexão ao MySql falhou. Tentando novamente em {1} segundos.", retryCount, timeSpan.Seconds);
                   });
           retryPolicy.Execute(() => { dbContext.Database.Migrate(); });
       }
   }
}
