using System.Collections.Concurrent;
using CalculatorServer.Library.Models;
using System.Diagnostics; 

namespace CalculatorService.Server.Services
{
	public class JournalService : IJournalService
	{
		private readonly ConcurrentDictionary<string, List<JournalEntry>> _journal = new();

		public void AddEntry(string trackingId, string operation, string calculation)
		{
			if (string.IsNullOrWhiteSpace(trackingId)) return;

			var entry = new JournalEntry
			{
				operation = operation,  
				calculation = calculation,   
				Date = DateTime.Now
			};

			_journal.AddOrUpdate(trackingId,
				new List<JournalEntry> { entry },
				(_, existingList) =>
				{
					existingList.Add(entry);
					return existingList;
				});

			Debug.WriteLine($"Entry added for {trackingId}. Total entries: {_journal[trackingId].Count}");
		}

		public List<JournalEntry> GetEntries(string trackingId)
		{
			if (string.IsNullOrWhiteSpace(trackingId))
				return new List<JournalEntry>();

			Debug.WriteLine($"Requested entries for: {trackingId}");
			Debug.WriteLine($"Available IDs: {string.Join(", ", _journal.Keys)}");

			return _journal.TryGetValue(trackingId, out var entries)
				? entries.OrderByDescending(e => e.Date).ToList()
				: new List<JournalEntry>();
		}
	}
}
