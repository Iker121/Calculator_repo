using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class JournalEntry
	{
		public string operation { get; set; } = string.Empty;
		public string calculation { get; set; } = string.Empty;
		public DateTime Date { get; set; }

	}

}
