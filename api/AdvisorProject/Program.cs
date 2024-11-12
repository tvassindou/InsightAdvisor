

using AdvisorProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AdvisorProject.Application;
using AdvisorProject.Filters;
using AdvisorProject.Extensions;
using AdvisorProject.ErrorHandling;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientApp",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Configure Serilog to write logs to a file in the "logs" folder
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AdvisorDbContext>(options =>
    options.UseInMemoryDatabase("AdvisorInMemoryDatabase"));

builder.Services.AddAutoMapper(typeof(AdvisorMappingProfile));

// Assemblies to scan for services and repositories
var assemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a.GetName().Name?.StartsWith("AdvisorProject") == true)
    .ToArray();
// Register all dependencies services and repositories
builder.Services.LoadDependencies(assemblies);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseWrapperFilter>();
});

var app = builder.Build();

// Use error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.InitialDbBuilder();

//  Don"t do this in production !!
app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();


app.UseCors("AllowClientApp");

app.Run();