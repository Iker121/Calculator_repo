using System;
using System.Collections.Generic;

namespace CalculatorService.Client.Models
{
	// Modelos para solicitudes (requests)
	public class AddRequest { public double[] Addends { get; set; } = Array.Empty<double>(); }
	public class SubtractRequest { public double Minuendo { get; set; } public double Substraendo { get; set; } }
	public class MultiplyRequest { public double[] Factors { get; set; } = Array.Empty<double>(); }
	public class DivideRequest { public double Dividendo { get; set; } public double Divisor { get; set; } }
	public class SquareRootRequest { public double Number { get; set; } }
	public class JournalQueryRequest { public string Id { get; set; } = string.Empty; }

	// Modelos para respuestas (responses)
	public class AddResponse { public double Sum { get; set; } }
	public class SubtractResponse { public double Difference { get; set; } }
	public class MultiplyResponse { public double Product { get; set; } }
	public class DivideResponse { public double Quotient { get; set; } public double Remainder { get; set; } }
	public class SquareRootResponse { public double Square { get; set; } }
	public class JournalQueryResponse { public List<JournalEntry> Operations { get; set; } = new List<JournalEntry>(); }

	// Modelo para entradas del journal
	public class JournalEntry
	{
		public string Operation { get; set; } = string.Empty;
		public string Calculation { get; set; } = string.Empty;
		public DateTime Date { get; set; }
	}

	// Modelo para errores
	public class ErrorResponse
	{
		public string ErrorCode { get; set; } = string.Empty;
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; } = string.Empty;
	}
}