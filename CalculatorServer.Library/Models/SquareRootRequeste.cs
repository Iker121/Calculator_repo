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
		[Required(ErrorMessage ="El numero es necesario")]
		[Range(0, double.MaxValue, ErrorMessage ="El numero no puede ser negativo")]
		public double numero { get; set; }
	}
}
