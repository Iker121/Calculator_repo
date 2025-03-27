using CalculatorWeb.Models;

namespace CalculatorWeb.Services
{
	public interface ICalculatorService
	{
		Task<double> AddAsync(double[] numbers);
		Task<double> SubtractAsync(double minuend, double subtrahend);
		Task<double> MultiplyAsync(double[] factors);
		Task<(double quotient, double remainder)> DivideAsync(double dividend, double divisor);
		Task<double> SquareRootAsync(double number);
	}

	public class CalculatorWebService : ICalculatorService
	{
		private readonly HttpClient _httpClient;

		public CalculatorWebService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<double> AddAsync(double[] numbers)
		{
			var response = await _httpClient.PostAsJsonAsync("calculator/add", new AddRequest { Sumandos = numbers });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<AddResponse>();
			return result.Sum;
		}

		public Task<(double quotient, double remainder)> DivideAsync(double dividend, double divisor)
		{
			throw new NotImplementedException();
		}

		public Task<double> MultiplyAsync(double[] factors)
		{
			throw new NotImplementedException();
		}

		public Task<double> SquareRootAsync(double number)
		{
			throw new NotImplementedException();
		}

		public async Task<double> SubtractAsync(double minuend, double subtrahend)
		{
			var response = await _httpClient.PostAsJsonAsync("calculator/sub",
				new { minuendo = minuend, substraendo = subtrahend });
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<SubstractResponse>();
			return result.Diferencia;
		}

		// Implementar otros métodos de manera similar...
	}
}
