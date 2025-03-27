using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library
{
	public class AddRequest
	{
		[Required(ErrorMessage = "The addends array is required")]
		[MinLength(2, ErrorMessage = "At least 2 numbers are required for the sum")]
		public double[] Addends { get; set; } //Array.Empty<double>();
	}
}