using CalculatorService.Client.Models;

namespace CalculatorService.Client.Services
{
    public interface ICalculatorClientService
    {
        Task<double> AddAsync(double[] sumandos);
        Task<double> SubstractAsync(double minuendo, double substraendo);
        Task<double> MultiplyAsync(double[] factores);
        Task<(double cociente, double resto)> DivideAsync(double dividendo, double divisor);
        Task<double> SquareRootAsync(double numero);
        Task<List<JournalEntry>> QueryJournalAsync(string trackingId);
		Task MultiplyAsync(double factores);
	}
}