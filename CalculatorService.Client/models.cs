using System.ComponentModel.DataAnnotations;

namespace CalculatorShared.Models
{
	public class AddRequest
	{
		[Required(ErrorMessage = "El array de sumandos es requerido")]
		[MinLength(2, ErrorMessage = "Se requieren al menos 2 números para la suma")]
		public double[] Sumandos { get; set; } = Array.Empty<double>();
	}

	public class DivideRequest
	{
		[Required(ErrorMessage = "El dividendo es requerido")]
		public double Dividendo { get; set; }

		[Required(ErrorMessage = "El divisor es requerido")]
		[Range(0.0001, double.MaxValue, ErrorMessage = "El divisor debe ser mayor a 0")]
		public double Divisor { get; set; }
	}

	public class MultiplyRequest
	{
		[Required(ErrorMessage = "El array de factores es requerido")]
		[MinLength(2, ErrorMessage = "Se requieren al menos 2 factores")]
		public double[] Factores { get; set; } = Array.Empty<double>();
	}
	public class SubtractRequest
	{
		[Required(ErrorMessage = "Minuendo es requerido")]
		public double Minuendo { get; set; }

		[Required(ErrorMessage = "Sustraendo es requerido")]
		public double Sustraendo { get; set; }
	}
	public class SquareRootRequest
	{
		[Required(ErrorMessage = "Número es requerido")]
		[Range(0, double.MaxValue, ErrorMessage = "Número no puede ser negativo")]
		public double Numero { get; set; }
	}
}
