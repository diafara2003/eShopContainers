using Ordering.API;
using Ordering.Application;
using Ordering.Infraestructure;
using Ordering.Infraestructure.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices()
    .AddInfraEstructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();


app.UserApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}



app.Run();


