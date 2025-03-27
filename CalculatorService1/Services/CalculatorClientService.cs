using CalculatorService.Client.Models;
using System.Net.Http.Json;

namespace CalculatorService.Client.Services
{
	public class CalculatorClientService : ICalculatorClientService
	{
		private readonly HttpClient _httpClient;

		public CalculatorClientService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<double> AddAsync(double[] sumandos)
		{
			var response = await _httpClient.PostAsJsonAsync("/calculator/add", new { sumandos = sumandos });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<AddResponse>();
			return result!.sum;
		}

		public async Task<double> SubstractAsync(double minuendo, double substraendo)
		{
			var response = await _httpClient.PostAsJsonAsync("/calculator/sub", new { Minuendo = minuendo, Substraendo = substraendo });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<SubstractResponse>();
			return result!.diferencia;
		}

		public async Task<double> MultiplyAsync(double[] factores)
		{
			var response = await _httpClient.PostAsJsonAsync("/calculator/mul", new { Factores = factores });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<MultiplyResponse>();
			return result!.producto;
		}

		public async Task<(double cociente, double resto)> DivideAsync(double dividendo, double divisor)
		{
			var response = await _httpClient.PostAsJsonAsync("/calculator/div", new { Dividendo = dividendo, Divisor = divisor });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<DivideResponse>();
			return (result!.cociente, result.resto);
		}

		public async Task<double> SquareRootAsync(double numero)
		{
			var response = await _httpClient.PostAsJsonAsync("/calculator/sqrt", new { Numero = numero });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<SquareRootResponse>();
			return result!.cuadrado;
		}

		public async Task<List<JournalEntry>> QueryJournalAsync(string trackingId)
		{
			var response = await _httpClient.PostAsJsonAsync("/calculator/journal", new { Id = trackingId });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<JournalQueryResponse>();
			return result!.operaciones;
		}

		public Task MultiplyAsync(double factores)
		{
			throw new NotImplementedException();
		}
	}
}