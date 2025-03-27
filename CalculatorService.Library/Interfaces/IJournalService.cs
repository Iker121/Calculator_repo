using System.Collections.Generic;
using CalculatorServer.Library.Models;

namespace CalculatorServerLibrary.Interfaces
{
	public interface IJournalService
	{
		void AddEntry(string trackingId, string operation, string calculation);
		List<JournalEntry> GetEntries(string trackingId);
	}
}