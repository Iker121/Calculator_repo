using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class SquareRootRequeste
	{
		[Required(ErrorMessage = "The number is required")]
		[Range(0, double.MaxValue, ErrorMessage = "The number cannot be negative")]
		public double number { get; set; }
	}
}
