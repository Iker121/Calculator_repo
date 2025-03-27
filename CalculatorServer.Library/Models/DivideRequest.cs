using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class DivideRequest
	{
		[Required(ErrorMessage = "The dividend is required")]
		public double dividend { get; set; }

		[Required(ErrorMessage = "The divisor is required")]
		[Range(0.0001, double.MaxValue, ErrorMessage = "The divisor must be greater than 0")]
		public double divisor { get; set; }
	}
}
