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
		[Required(ErrorMessage = "Minuendo necesario")]
		public double minuendo { get; set; }

		[Required(ErrorMessage = "Substraendo necesario")]
		public double substraendo { get; set; }
	}
}
