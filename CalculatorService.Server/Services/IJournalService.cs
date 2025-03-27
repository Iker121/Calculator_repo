using CalculatorServer.Library.Models;


namespace CalculatorService.Server.Services
{
	public interface IJournalService
	{
		void AddEntry(string trackingId, string operacion, string calculo);
		List<JournalEntry> GetEntries(string trackingId);
	}
}
