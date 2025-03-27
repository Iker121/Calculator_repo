using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class MultiplyRequest
	{
		[Required(ErrorMessage = "Values must be entered")]
		[MinLength(2, ErrorMessage = "A minimum of 2 factors are required")]
		public double[] factors { get; set; } = Array.Empty<double>();
	}
}
