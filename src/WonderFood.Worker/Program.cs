using System.Text.Json.Serialization;
using MassTransit;
using WonderFood.Application;
using WonderFood.Infra.Sql;
using WonderFood.Worker;
using WonderFood.Worker.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddSqlInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
var rabbitMqUser = "useradmin";
var rabbitMqPassword = "senhaForte123!";
var rabbitMqHost = "amqp://wonderfood_mq:5672";
            
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<PagamentoProcessadoConsumer>();
    busConfigurator.AddConsumer<StatusPedidoAlteradoConsumer>();
    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, hst =>
        {
            hst.Username(rabbitMqUser);
            hst.Password(rabbitMqPassword);
        });

        cfg.ReceiveEndpoint("pagamento_processado", e =>
        {
            e.ConfigureConsumer<PagamentoProcessadoConsumer>(context);
            e.Bind("WonderFood.Models.Events:PagamentoProcessadoEvent", x =>
            {
                x.RoutingKey = "pagamento.processado";
                x.ExchangeType = "fanout";
            });
        });
        
        cfg.ReceiveEndpoint("status_alterado", e =>
        {
            e.ConfigureConsumer<StatusPedidoAlteradoConsumer>(context);
            e.Bind("WonderFood.Models.Events:StatusPedidoAlteradoEvent", x =>
            {
                x.RoutingKey = "status.alterado";
                x.ExchangeType = "fanout";
            });
        });
    });
});


var app = builder.Build();

app.UseSwaggerMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();