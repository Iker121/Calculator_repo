﻿// En CalculatorWebApp/Models/ErrorViewModel.cs
namespace CalculatorWeb.Models
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}