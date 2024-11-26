
using Basket.API.Data;
using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("Database")!);
    o.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
// Add services to the container 

builder.Services.AddMediatR(cf =>
{
    cf.RegisterServicesFromAssembly(assembly);
    cf.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cf.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services
    .AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(_ => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();

