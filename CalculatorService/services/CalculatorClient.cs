using CalculatorService.Library.Interfaces;
using CalculatorService.Library.Models;
using System.Net.Http.Headers;

namespace CalculatorService.Client.Services
{
	public class CalculatorClient : ICalculatorService
	{
		private readonly HttpClient _http;
		private string? _trackingId;

		public CalculatorClient(HttpClient http) => _http = http;

		public void SetTrackingId(string trackingId) => _trackingId = trackingId;

		public async Task<double> Add(double[] addends)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, "/calculator/add")
			{
				Content = JsonContent.Create(new AddRequest { Addends = addends })
			};
			AddTrackingHeader(request);

			var response = await _http.SendAsync(request);
			return (await response.Content.ReadFromJsonAsync<AddResponse>())!.Sum;
		}

		private void AddTrackingHeader(HttpRequestMessage request)
		{
			if (!string.IsNullOrEmpty(_trackingId))
				request.Headers.Add("X-Evi-Tracking-Id", _trackingId);
		}

		// Implementa Subtract, Multiply, etc. de forma similar...
	}
}