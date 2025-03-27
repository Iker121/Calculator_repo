using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class JournalQueryResponse
	{
		public List<JournalEntry> operations { get; set; } = new();
	}
}
