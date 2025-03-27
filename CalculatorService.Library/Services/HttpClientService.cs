using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServerLibrary.Services
{
	public class HttpClientService
	{
		protected readonly HttpClient _httpClient;
		protected string _trackingId;

		public HttpClientService(string baseUrl)
		{
			_httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public void SetTrackingId(string trackingId) => _trackingId = trackingId;

		protected async Task<TResponse> PostAsync<TResponse>(string endpoint, object request)
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
				throw new Exception($"Error {error.ErrorStatus}: {error.ErrorMessage}");
			}

			return JsonConvert.DeserializeObject<TResponse>(responseContent);
		}
	}
}