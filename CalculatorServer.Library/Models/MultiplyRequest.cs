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
		[Required(ErrorMessage = "Es necesario insertar valores")]
		[MinLength(2, ErrorMessage = "Se necesitan minimo 2 factores")]
		public double[] factors { get; set; } = Array.Empty<double>();
	}
}
