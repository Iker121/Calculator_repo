//using CalculatorService.Server.Models;
using CalculatorServer.Library;
using CalculatorServer.Library.Models;
using CalculatorService.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.Controllers
{
	[ApiController]
	[Route("calculator")]
	public class CalculatorController : ControllerBase
	{
		private readonly ICalculatorService _calculator;
		private readonly IJournalService _journal;
		public CalculatorController(ICalculatorService calculator, IJournalService journal)
		{
			_calculator = calculator;
			_journal = journal;
		}
		[HttpPost("add")]
		public IActionResult Add([FromBody] AddRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trackingId = null)
		{
			if (!ModelState.IsValid) 
				return BadRequest(ModelState);
			try
			{
				var result = _calculator.Add(request.Sumandos);
				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Add", $"{string.Join("+", request.Sumandos)} = {result}");
				}
				return Ok(new AddResponse { Sum = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
			}
		}
		[HttpPost("sub")]
		public IActionResult Substract([FromBody] SubstractRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trackingId = null)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var result = _calculator.Substract(request.minuendo, request.substraendo);
				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Substract", $"{request.minuendo} - {request.substraendo} = {result}");
				}
				return Ok(new SubstractResponse { Diferencia = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
			}
		}
		[HttpPost("mul")]
		public IActionResult Multiply([FromBody] MultiplyRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trackingId = null)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var result = _calculator.Multiply(request.factors);

				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Multiply", $" {string.Join(" * ", request.factors)} = {result}");
				}
				return Ok(new MultiplyResponse { producto = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
			}

		}
		[HttpPost("div")]
		public IActionResult Divide([FromBody] DivideRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trackingId = null)
		{

			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var result = _calculator.Divide(request.dividendo, request.divisor);

				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Divide", $"{request.dividendo} / {request.divisor} = {result.cociente} (Resto: {result.resto})");
				}
				return Ok(new DivideResponse { cociente = result.cociente, resto = result.resto });
			}
			catch (Exception ex)
			{
				return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
			}
		}
		[HttpPost("sqrt")]
		public IActionResult SquareRoot([FromBody] SquareRootRequeste requeste, [FromHeader(Name = "X-Evi-Tracking-Id")] string trackingId = null)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var result = _calculator.SquareRoot(requeste.numero);

				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "SquareRoot", $"√{requeste.numero} = {result}");
				}

				return Ok(new SquareRootResponse { cuadrado = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
			}
		}
		[HttpPost("journal")]
		public IActionResult JournalQuery([FromBody] JournalQueryRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (request == null || string.IsNullOrWhiteSpace(request.Id))
			{
				return BadRequest(new ErrorResponse
				{
					ErrorStatus = 400,
					ErrorMessage = "Se requiere un ID de tracking válido"
				});
			}

			try
			{
				var entries = _journal.GetEntries(request.Id);
				return Ok(new JournalQueryResponse
				{
					operaciones = entries ?? new List<JournalEntry>()
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ErrorResponse
				{
					ErrorStatus = 500,
					ErrorMessage = $"Error interno: {ex.Message}"
				});
			}
		}
	}
}
