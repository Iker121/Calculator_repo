using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServer.Library.Models
{
	public class ErrorResponse
	{
		public string ErrorCode { get; set; } = string.Empty;
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; } = string.Empty;
	}
}
