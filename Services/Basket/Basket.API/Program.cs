

using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using Discount.Grpc;
using HealthChecks.UI.Client;
using JasperFx;
using Microsoft.Extensions.DependencyInjection;
using static Discount.Grpc.DiscountProtoService;


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
    options.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisCnn;
    //options.InstanceName = "Basket";
});

//grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler= new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});
;


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
