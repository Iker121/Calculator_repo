using CalculatorServer.Library.Models;


namespace CalculatorService.Server.Services
{
	public interface IJournalService
	{
		void AddEntry(string trackingId, string operation, string calculation);
		List<JournalEntry> GetEntries(string trackingId);
	}
}
