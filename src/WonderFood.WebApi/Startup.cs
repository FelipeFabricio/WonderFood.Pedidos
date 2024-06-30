using System.Text.Json.Serialization;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
using WonderFood.Application;
using WonderFood.Application.Sagas;
using WonderFood.Infra.Sql;
using WonderFood.Infra.Sql.Context;
using WonderFood.Models.Events;

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
            services.AddApplication();
            services.AddSqlInfrastructure(Configuration);
            services.AddSwagger();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var rabbitMqUser = Configuration["RABBITMQ_DEFAULT_USER"];
            var rabbitMqPassword = Configuration["RABBITMQ_DEFAULT_PASS"];
            var rabbitMqHost = Configuration["RABBITMQ_HOST"];
            
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumers(typeof(Program).Assembly);
                busConfigurator.AddSagaStateMachine<CriarPedidoStateMachine, CriarPedidoSagaState>()
                    .EntityFrameworkRepository(r =>
                    {
                        r.ExistingDbContext<WonderFoodContext>();
                        r.UseMySql();
                    });
            
                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqHost, hst =>
                    {
                        hst.Username(rabbitMqUser);
                        hst.Password(rabbitMqPassword);
                    });

                    cfg.Publish<PagamentoSolicitadoEvent>(x =>
                    {
                        x.ExchangeType = "fanout";
                    });
                    
                    cfg.Publish<IniciarProducaoCommand>(x =>
                    {
                        x.ExchangeType = "fanout";
                    });
                    
                    cfg.Publish<ReembolsoSolicitadoEvent>(x =>
                    {
                        x.ExchangeType = "fanout";
                    });
                    
                    cfg.UseInMemoryOutbox(context);
                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WonderFoodContext dbContext)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });
            app.UseSwaggerMiddleware();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            ExecuteDatabaseMigration(dbContext);
            SeedDatabase(app);
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
                        Log.Logger.Error(
                            "Tentativa {0} de conexão ao MySql falhou. Tentando novamente em {1} segundos.", retryCount,
                            timeSpan.Seconds);
                    });
            retryPolicy.Execute(() => { dbContext.Database.Migrate(); });
        }
        
        private void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }
    }
}