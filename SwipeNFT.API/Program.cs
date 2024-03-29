using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SwipeNFT.API.Extensions;
using SwipeNFT.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks();

builder.RegisterIoC()
    .RegisterSerilogLoggingServices()
    .RegisterJWTServices()
    .RegisterAuthenticationContextServices()
    .RegisterSwaggerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Configuration.GetValue<bool>("AppSettings:SwaggerEnabled"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwipeNFT API v.1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHealthChecks("/health");

app.MapControllers();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder
        .WithOrigins(builder.Configuration.GetValue<string>("AppSettings:ClientUrl"))
        .AllowAnyHeader()
        .AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<CustomExceptionHandlingMiddleware>();

try
{
    Log.Information("Starting web host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

