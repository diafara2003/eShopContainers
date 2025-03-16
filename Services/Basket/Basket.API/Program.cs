

using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;


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

var app = builder.Build();

//builder.Services.AddExceptionHandler<CustomExceptionHandler>();


app.MapCarter();

app.UseExceptionHandler(options =>
{

});

//app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
//{

//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.Run();
