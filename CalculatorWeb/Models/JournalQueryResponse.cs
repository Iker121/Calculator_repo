using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWeb.Models
{
	public class JournalQueryResponse
	{
		public List<JournalEntry> operaciones { get; set; } = new();
	}
}
