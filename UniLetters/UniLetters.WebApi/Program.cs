using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints()
    .AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapFastEndpoints();
app.Run();