using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Endpoints.Constraints;
using UniLetters.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddDbContext<UniLettersDbContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("Database"))
    .EnableSensitiveDataLogging());

builder.Services
    .AddScoped<StudentService>()
    .AddHostedService<StartupService>()
    .AddFastEndpoints()
    .AddOpenApi();

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("am", typeof(AmConstraint));
});

var app = builder.Build();

app.MapOpenApi();
app.MapFastEndpoints();
app.Run();