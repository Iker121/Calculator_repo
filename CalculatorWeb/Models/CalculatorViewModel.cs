// En CalculatorWebApp/Models/CalculatorViewModel.cs
using System.Collections.Generic;

namespace CalculatorWeb.Models
{
	public class CalculatorViewModel
	{
		public string Input { get; set; }
		public string Operation { get; set; } = "add";
		public double Result { get; set; }
		public string ErrorMessage { get; set; }
		public List<string> History { get; set; } = new List<string>();
	}
}