using System.Reflection;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Polly;
using Serilog;
using WonderFood.Application;
using WonderFood.Infra.Bus;
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
           services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
               });

           services.AddEndpointsApiExplorer();
           services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "WonderFood", Version = "v1" });
               var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               c.IncludeXmlComments(xmlPath);
           });
           services.AddApplication();
           services.AddSqlInfrastructure(Configuration);
           services.AddAzureServiceBusServices(Configuration);
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
       }
       
       public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WonderFoodContext dbContext)
       {
           app.UseSwagger();
           app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WonderFood"));
           app.UseRouting();
           app.UseAuthorization();
           app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();
               endpoints.MapGet("/_health", () => Results.Ok("Healthy"));
               endpoints.MapHealthChecks("/_ready", new HealthCheckOptions
               {
                   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                   ResultStatusCodes =
                   {
                       [HealthStatus.Healthy] = StatusCodes.Status200OK,
                       [HealthStatus.Degraded] = StatusCodes.Status200OK,
                       [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                   }
               });
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
                       Log.Logger.Error($"Tentativa {retryCount} de conexão ao MySql falhou. Tentando novamente em {timeSpan.Seconds} segundos.");
                   });
           retryPolicy.Execute(() => { dbContext.Database.Migrate(); });
       }
   }
}
