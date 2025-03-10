using Business.Extensions;
using Data.Extensions;
using Serilog;
using WebApi.Extensions;
using WebApi.Middlewares;
using WebApi.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

var connectionStrings = builder.Configuration
    .GetSection(nameof(ConnectionStrings))
    .Get<ConnectionStrings>();

builder.Services.AddDataLayer(c =>
{
    c.ConnectionString = connectionStrings.DefaultConnection;
    c.EnableSensitiveDataLogging = builder.Environment.IsDevelopment();
});

builder.Services.AddBusinessLayer();

builder.Services.AddWebApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseRouting();

app.UseMiddleware<IpAddressLoggerMiddleware>();
app.UseMiddleware<PerformanceLoggerMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.InitializeDb();

app.Run();