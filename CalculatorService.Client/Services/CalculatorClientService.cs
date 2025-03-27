using CalculatorServer.Library.Models;
using CalculatorServer.Library;
using CalculatorShared.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonException = System.Text.Json.JsonException;

namespace CalculatorClient.Services
{
	public class CalculatorApiClient
	{
		private readonly HttpClient _httpClient;
		private string _trackingId;

		public CalculatorApiClient(string baseUrl)
		{
			_httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public void SetTrackingId(string trackingId) => _trackingId = trackingId;

		// Suma
		public async Task<double> AddAsync(double[] numbers)
		{
			var request = new AddRequest { Sumandos = numbers };
			var response = await PostAsync<AddResponse>("calculator/add", request);
			return response.Sum;
		}

		// Resta
		public async Task<double> SubtractAsync(double minuend, double subtrahend)
		{
			var request = new SubstractRequest { minuendo = minuend, substraendo = subtrahend };
			var response = await PostAsync<SubstractResponse>("calculator/sub", request);
			return response.Diferencia;
		}

		// Multiplicación
		public async Task<double> MultiplyAsync(double[] factors)
		{
			var request = new MultiplyRequest { factores = factors };
			var response = await PostAsync<MultiplyResponse>("calculator/mul", request);
			return response.producto;
		}

		// División (devuelve cociente y resto)
		public async Task<(double Quotient, double Remainder)> DivideAsync(double dividend, double divisor)
		{
			var request = new DivideRequest { dividendo = dividend, divisor = divisor };
			var response = await PostAsync<DivideResponse>("calculator/div", request);
			return (response.cociente, response.resto);
		}

		// Raíz cuadrada
		public async Task<double> SquareRootAsync(double number)
		{
			var request = new SquareRootRequeste { numero = number };
			var response = await PostAsync<SquareRootResponse>("calculator/sqrt", request);
			return response.cuadrado;
		}

		// Consulta de journal
		public async Task<JournalQueryResponse> QueryJournalAsync()
		{
			if (string.IsNullOrEmpty(_trackingId))
				throw new InvalidOperationException("Tracking ID no configurado");

			var request = new JournalQueryRequest { Id = _trackingId };
			return await PostAsync<JournalQueryResponse>("calculator/journal", request);
		}

		// Método base para todas las peticiones HTTP
		private async Task<TResponse> PostAsync<TResponse>(string endpoint, object request)
		{
			try
			{
				var json = JsonConvert.SerializeObject(request);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				if (!string.IsNullOrEmpty(_trackingId))
					content.Headers.Add("X-Evi-Tracking-Id", _trackingId);

				var response = await _httpClient.PostAsync(endpoint, content);
				var responseContent = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					var error = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
					throw new HttpRequestException($"HTTP {error.ErrorStatus}: {error.ErrorMessage}");
				}

				return JsonConvert.DeserializeObject<TResponse>(responseContent);
			}
			catch (JsonException ex)
			{
				throw new Exception("Error al procesar la respuesta JSON", ex);
			}
			catch (TaskCanceledException ex)
			{
				throw new Exception("Timeout: El servidor no respondió a tiempo", ex);
			}
		}
	}
}