using System.Collections.Concurrent;
using CalculatorService.Server.Models;

namespace CalculatorService.CalculatorService.Server.Services
{
    public class Journal : IJournal
    {
        private readonly ConcurrentDictionary<string, List<JournalEntry>> _journal = new();

        public void AddEntry(string trackingId, string operaciones, string calculo)
        {
            var entry = new JournalEntry
            {
                Operaciones = operaciones,
                Calculo = calculo,
                Date = DateTime.UtcNow
            };

            _journal.AddOrUpdate(trackingId, new List<JournalEntry> { entry }, (key, existingList) =>
            {
                existingList.Add(entry);
                return existingList;
            });
        }

        public List<JournalEntry> GetEntries(string trackingId)
        {
            return _journal.TryGetValue(trackingId, out var entries) ? entries : new List<JournalEntry>();
        }
    }
}
