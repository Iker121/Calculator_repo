using CalculatorService.Server.Models;

namespace CalculatorService.CalculatorService.Server.Services
{
    public interface IJournal
    {
        void AddEntry(string trackingId, string operacion, string calculo);
        List<JournalEntry> GetEntries(string trackingId);
    }
}
