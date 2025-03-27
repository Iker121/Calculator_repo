using CalculatorService.Client.Models;
using CalculatorService.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Client.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CalculatorController : ControllerBase
	{
		private readonly ICalculatorClientService _calculatorService;

		public CalculatorController (ICalculatorClientService calculatorService)
		{
			_calculatorService = calculatorService;
		}
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] double[] sumandos)
		{
			var result = await _calculatorService.AddAsync(sumandos);
			return Ok(result);
		}
		[HttpPost("sub")]
		public async Task<IActionResult> Substract([FromBody]SubstractRequest request)
		{
			var result = await _calculatorService.SubstractAsync(request.minuendo, request.substraendo);
			return Ok(result);
		}
		[HttpPost("mul")]
		public async Task<IActionResult> Multiply([FromBody] double[] factores)
		{
			var result = await _calculatorService.MultiplyAsync(factores);
			return Ok(result);
		}
		[HttpPost("div")]
		public async Task<IActionResult> Divide([FromBody] DivideRequest request)
		{
			var result = await _calculatorService.DivideAsync(request.dividendo, request.divisor);
			return Ok(result);
		}
		[HttpPost("sqrt")]
		public async Task <IActionResult> SquareRoot([FromBody] double numero)
		{
			var result = await _calculatorService.SquareRootAsync(numero);
			return Ok(result);
		}
		[HttpPost ("journal/query")]
		public async Task<IActionResult> QueryJournal([FromBody]JournalQueryRequest request)
		{
			var entries = await _calculatorService.QueryJournalAsync(request.id);
			return Ok(entries);
		}

	}
}
