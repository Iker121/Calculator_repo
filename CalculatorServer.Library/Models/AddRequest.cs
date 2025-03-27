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
		[Required(ErrorMessage = "El array de sumandos es requerido")]
		[MinLength(2, ErrorMessage = "Se requieren al menos 2 numeros para la suma")]
		public double[] Sumandos { get; set; } //Array.Empty<double>();
	}
}