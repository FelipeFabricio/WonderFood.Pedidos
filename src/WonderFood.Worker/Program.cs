using WonderFood.Application;
using WonderFood.ExternalServices;
using WonderFood.Infra.Sql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ExternalServicesSettings>(builder.Configuration.GetSection("ExternalServicesSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddExternalServices();
builder.Services.AddSqlInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();