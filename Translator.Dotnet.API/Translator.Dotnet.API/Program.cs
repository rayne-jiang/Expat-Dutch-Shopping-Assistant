using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Translator.Dotnet.API.Data;
using DotNetEnv;
using Translator.Dotnet.API.Services;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:5174")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IJumboProductService, JumboProductService>();
builder.Services.AddScoped<TranslatorService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Use CORS policy
app.UseCors("AllowFrontend");

app.UseRouting();

app.MapControllers();

app.Run();

