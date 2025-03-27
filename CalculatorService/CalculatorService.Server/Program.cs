using CalculatorService.Client.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios
builder.Services.AddHttpClient<ICalculatorClientService, CalculatorClientService>(client =>
{
	client.BaseAddress = new Uri("http://localhost:5000"); // URL del servidor
});

// Configurar controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configurar el pipeline de la aplicación
app.UseRouting();
app.MapControllers();

app.Run();