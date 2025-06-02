using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UniLettersDbContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("Database"))
    .EnableSensitiveDataLogging());

builder.Services
    .AddHostedService<StartupService>()
    .AddFastEndpoints()
    .AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapFastEndpoints();
app.Run();