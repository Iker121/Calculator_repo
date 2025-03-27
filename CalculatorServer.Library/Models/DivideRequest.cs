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
		[Required(ErrorMessage = "El dividendo es necesario")]
		public double dividendo { get; set; }

		[Required(ErrorMessage = "El divisor es necesario")]
		[Range(0.0001, double.MaxValue, ErrorMessage ="El divisor tiene que ser mayor que 0")]
		public double divisor { get; set; }
	}
}
