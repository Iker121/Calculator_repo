using System.Net.Http.Headers;
using System.Text;
using CalculatorServer.Library;
using CalculatorServer.Library.Models;
using Newtonsoft.Json;

namespace CalculatorService.Client.Services
{
	public class CalculatorApiClient
	{
		private readonly HttpClient _httpClient;
		private string _trackingId;

		public CalculatorApiClient (string baseUrl)
		{
			_httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}
		public void SetTrackingId (string trackingId) => _trackingId = trackingId;

		public async Task<double> AddAsync (double[] numbers)
		{
			var request = new AddRequest {  Addends = numbers };
			var response = await PostAsync <AddResponse>("calculator/add", request);
			return response.Add;
		}

		public async Task<double> SubstractAsync(double minuend, double subtrahend)
		{
			var request = new SubstractRequest { minuend = minuend, subtrahend = subtrahend };
			var response = await PostAsync <SubstractResponse> ("calculator/sub",  request);
			return response.Diference;
		}

		public async Task<double> MultiplyAsync(double[] factors)
		{
			var request = new MultiplyRequest { factors = factors };
			var response = await PostAsync<MultiplyResponse>("calculator/mul", request);
			return response.product;
		}

		public async Task<(double quotient, double remainder)> DivideAsync(double dividend, double divisor)
		{
			var request = new DivideRequest { dividend = dividend, divisor = divisor };
			var response = await PostAsync<DivideResponse>("calculator/div", request);
			return (response.quotient, response.remainder);
		}

		public async Task<double> SquareRootAsync(double number)
		{
			var request = new SquareRootRequeste { number = number };
			var response = await PostAsync<SquareRootResponse>("calculator/sqrt", request);
			return response.square;
		}

		public async Task<JournalQueryResponse> QueryJournalAsync(string trackingId)
		{
			var request = new JournalQueryRequest { Id = trackingId };
			Console.WriteLine($"Enviando consulta journal para el Id: {trackingId}");

			try
			{
				return await PostAsync<JournalQueryResponse>("calculator/journal", request);
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine($"Detalles del error: {ex.Message}");
				throw;
			}
		}
		private async Task<TResponse> PostAsync<TResponse>(string endpoint, object request)
		{
			try
			{
				var json = JsonConvert.SerializeObject(request);
				Console.WriteLine($"Enviando JSON: {json}");
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				if (!string.IsNullOrEmpty(_trackingId))
					content.Headers.Add("X-Evi-Tracking-Id", _trackingId);

				var response = await _httpClient.PostAsync(endpoint, content);
				var responseContent = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					var error = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
					throw new HttpRequestException($"HTTP {error.ErrorStatus}:{error.ErrorMessage}");
				}
				return JsonConvert.DeserializeObject<TResponse>(responseContent);
			}
			catch (JsonException ex)
			{
				throw new Exception("JSON processing error", ex);
			}
			catch (TaskCanceledException ex)
			{
				throw new Exception("Server is not responding", ex);
			}
		}
	}
}
