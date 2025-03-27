using CalculatorService.Client.Models;
using System.Threading.Tasks;

namespace CalculatorService.Client.Services
{
	public interface ICalculatorClientService
	{
		Task<double> AddAsync(double[] addends);
		Task<double> SubtractAsync(double minuendo, double substraendo);
		Task<double> MultiplyAsync(double[] factors);
		Task<(double Quotient, double Remainder)> DivideAsync(double dividendo, double divisor);
		Task<double> SquareRootAsync(double number);
		Task<List<JournalEntry>> QueryJournalAsync(string trackingId);
	}
}