

using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using HealthChecks.UI.Client;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LogginBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

string cnn = builder.Configuration.GetConnectionString("DataBase")!;
string redisCnn = builder.Configuration.GetConnectionString("Redis")!;
builder.Services.AddMarten(options =>
{
    options.Connection(cnn);
    options.Schema.For<ShoppingCartRoot>().Identity(x => x.UserName);
    //options.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisCnn;
    //options.InstanceName = "Basket";
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(cnn)
.AddRedis(redisCnn);

var app = builder.Build();




app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{

    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
