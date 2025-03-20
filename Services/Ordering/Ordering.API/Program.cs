using Ordering.API;
using Ordering.Application;
using Ordering.Infraestructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices()
    .AddInfraEstructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();


app.UserServices();

app.Run();
