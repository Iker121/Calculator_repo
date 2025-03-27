using CalculatorService.Client.Models;
using CalculatorService.Client.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CalculatorService.Client.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CalculatorController : ControllerBase
	{
		private readonly ICalculatorClientService _calculatorService;

		public CalculatorController(ICalculatorClientService calculatorService)
		{
			_calculatorService = calculatorService;
		}

		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] double[] addends)
		{
			var result = await _calculatorService.AddAsync(addends);
			return Ok(result);
		}

		[HttpPost("sub")]
		public async Task<IActionResult> Subtract([FromBody] SubtractRequest request)
		{
			var result = await _calculatorService.SubtractAsync(request.Minuendo, request.Substraendo);
			return Ok(result);
		}

		[HttpPost("mul")]
		public async Task<IActionResult> Multiply([FromBody] double[] factors)
		{
			var result = await _calculatorService.MultiplyAsync(factors);
			return Ok(result);
		}

		[HttpPost("div")]
		public async Task<IActionResult> Divide([FromBody] DivideRequest request)
		{
			var result = await _calculatorService.DivideAsync(request.Dividendo, request.Divisor);
			return Ok(result);
		}

		[HttpPost("sqrt")]
		public async Task<IActionResult> SquareRoot([FromBody] double number)
		{
			var result = await _calculatorService.SquareRootAsync(number);
			return Ok(result);
		}

		[HttpPost("journal/query")]
		public async Task<IActionResult> QueryJournal([FromBody] JournalQueryRequest request)
		{
			var entries = await _calculatorService.QueryJournalAsync(request.Id);
			return Ok(entries);
		}
	}
}