using CalculatorService.CalculatorService.Server.Logging;
using CalculatorService.CalculatorService.Server.Middleware;
using CalculatorService.CalculatorService.Server.Services;
using Microsoft.Extensions.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);

Logging.CongigureLogging(builder);

builder.Services.AddSingleton<ICalculator, Calculator>();
builder.Services.AddSingleton<IJournal, Journal>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<Middleware>();

app.MapControllers();

app.Run();
