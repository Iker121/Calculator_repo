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
		[Required]
		[MinLength(2)]
		public double[] Sumandos { get; set; } //Array.Empty<double>();
	}
}