using Microsoft.OpenApi.Models;
using StackOverflowTags.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StackOverflowTags API", Description = "Utilities for StackOverflow's Tags", Version = "v1" });
});
builder.Services.AddLogging(config =>
    config
        .AddDebug()
        .AddConsole()
        .AddConfiguration(builder.Configuration)
        .SetMinimumLevel(LogLevel.Information));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StackOverflowTags API V1");
});

app.MapGet("/", () => "Hello World!");

app.Run();
