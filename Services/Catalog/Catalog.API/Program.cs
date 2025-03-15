using Catalog.API.Data;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;
using HealthChecks.UI.Client;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddCarter (configurator: c =>
{
    c.WithModule<CreateProductEndpoint>();
    c.WithModule<GetProductsEndpoint>();
    c.WithModule<GetProductByIdEndpoint>();
    c.WithModule<DeleteProductEndpoint>();
    c.WithModule<GetProductByCategoryEndpoint>();
    c.WithModule<UpdateProductEndpoint>();

});
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LogginBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DataBase")!);    
    //options.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
string cnn = builder.Configuration.GetConnectionString("DataBase")!;
builder.Services.AddHealthChecks()
    .AddNpgSql(cnn);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options =>
{

});

app.UseHealthChecks("/health",new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{

    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
