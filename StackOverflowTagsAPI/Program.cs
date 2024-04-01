using Microsoft.OpenApi.Models;
using StackOverflowTags.Application.Extensions;
using StackOverflowTags.Domain.Interfaces;
using StackOverflowTags.Infrastructure.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StackOverflowTags API", Description = "Utilities for StackOverflow's Tags", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddLogging(config =>
    config
        .AddDebug()
        .AddConsole()
        .AddConfiguration(builder.Configuration)
        .SetMinimumLevel(LogLevel.Information));
builder.Services.AddControllers();

var app = builder.Build();

var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<ITagTableInitializer>();
await initializer.Initialize();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StackOverflowTags API V1");
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
