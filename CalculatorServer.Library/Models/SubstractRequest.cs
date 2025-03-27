using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class SubstractRequest
	{
		[Required(ErrorMessage = "The minuend must be provided")]
		public double minuend { get; set; }

		[Required(ErrorMessage = "The subtrahend must be specified")]
		public double subtrahend { get; set; }
	}
}
