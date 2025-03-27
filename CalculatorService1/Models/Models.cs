namespace CalculatorService.Client.Models
{
	public class AddRequest
	{
		public double[] sumandos {  get; set; } = Array.Empty<double>(); 
	}
	public class AddResponse
	{
		public double sum {  get; set; }
	}

	public class SubstractRequest
	{
		public double minuendo {  get; set; } 
		public double substraendo { get; set; }
	}
	public class SubstractResponse
	{
		public double diferencia { get; set; }
	}
	public class MultiplyRequest
	{
		public double[] factores { get; set; } = Array.Empty<double>();
	}
	public class MultiplyResponse
	{
		public double producto { get; set; }
	}
	public class DivideRequest
	{
		public double dividendo { get; set; }
		public double divisor { get; set; }
	}
	public class DivideResponse
	{
		public double cociente { get; set; } 
		public double resto	{ get; set; }
	}
	public class SquareRootRequeste
	{
		public double numero { get; set; }
	}
	public class SquareRootResponse
	{
		public double cuadrado	{ get; set; }
	}
	public class JournalQueryRequest
	{
		public string id { get; set; }	= string.Empty;
	}
	public class JournalQueryResponse
	{
		public List<JournalEntry> operaciones { get; set; } = new List<JournalEntry>();
	}
	public class JournalEntry
	{
		public string operacion {  get; set; } = string.Empty ;
		public string calculo {  get; set; } = string.Empty;
		public DateTime date { get; set; }
	}
	public class ErrorResponse
	{
		public string ErrorCode { get; set; } = string.Empty;
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; } = string.Empty;
	}
}

