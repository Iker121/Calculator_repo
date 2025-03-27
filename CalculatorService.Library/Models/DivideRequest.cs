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
		public double dividendo { get; set; }
		[Range(1, double.MaxValue)]
		public double divisor { get; set; }
	}
}
