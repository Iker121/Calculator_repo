using CalculatorService.Client.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CalculatorService.Client.Services
{
    public class CalculatorClientService : ICalculatorClientService
    {
        private readonly HttpClient _httpClient;

        public CalculatorClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> AddAsync(double[] addends)
        {
            var response = await _httpClient.PostAsJsonAsync("/calculator/add", new { Addends = addends });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<AddResponse>();
            return result!.Sum;
        }

        public async Task<double> SubtractAsync(double minuendo, double substraendo)
        {
            var response = await _httpClient.PostAsJsonAsync("/calculator/sub", new { Minuendo = minuendo, Substraendo = substraendo });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<SubtractResponse>();
            return result!.Difference;
        }

        public async Task<double> MultiplyAsync(double[] factors)
        {
            var response = await _httpClient.PostAsJsonAsync("/calculator/mul", new { Factors = factors });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<MultiplyResponse>();
            return result!.Product;
        }

        public async Task<(double Quotient, double Remainder)> DivideAsync(double dividendo, double divisor)
        {
            var response = await _httpClient.PostAsJsonAsync("/calculator/div", new { Dividendo = dividendo, Divisor = divisor });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<DivideResponse>();
            return (result!.Quotient, result.Remainder);
        }

        public async Task<double> SquareRootAsync(double number)
        {
            var response = await _httpClient.PostAsJsonAsync("/calculator/sqrt", new { Number = number });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<SquareRootResponse>();
            return result!.Square;
        }

        public async Task<List<JournalEntry>> QueryJournalAsync(string trackingId)
        {
            var response = await _httpClient.PostAsJsonAsync("/journal/query", new { Id = trackingId });
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<JournalQueryResponse>();
            return result!.Operations;
        }
    }
}