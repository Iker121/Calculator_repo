using CalculatorService.Library.Interfaces;
using CalculatorService.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<ICalculatorService, CalculatorClient>(client =>
{
	client.BaseAddress = new Uri("https://localhost:5000");
});

var app = builder.Build();
app.MapControllers();
app.Run();