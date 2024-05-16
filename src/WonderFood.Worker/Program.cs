using System.Text.Json.Serialization;
using WonderFood.Application;
using WonderFood.ExternalServices;
using WonderFood.Infra.Sql;
using WonderFood.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ExternalServicesSettings>(builder.Configuration.GetSection("ExternalServicesSettings"));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddExternalServices();
builder.Services.AddSqlInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseSwaggerMiddleware();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();