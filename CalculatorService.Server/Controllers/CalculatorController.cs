
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
				var result = _calculator.Add(request.Addends);
				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Add", $"{string.Join("+", request.Addends)} = {result}");
				}
				return Ok(new AddResponse { Add = result });
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
				var result = _calculator.Substract(request.minuend, request.subtrahend);
				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Substract", $"{request.minuend} - {request.subtrahend} = {result}");
				}
				return Ok(new SubstractResponse { Diference = result });
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
				return Ok(new MultiplyResponse { product = result });
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
				var result = _calculator.Divide(request.dividend, request.divisor);

				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "Divide", $"{request.dividend} / {request.divisor} = {result.quotient} (Remainder: {result.remainder})");
				}
				return Ok(new DivideResponse { quotient = result.remainder, remainder = result.remainder });
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
				var result = _calculator.SquareRoot(requeste.number);

				if (!string.IsNullOrEmpty(trackingId))
				{
					_journal.AddEntry(trackingId, "SquareRoot", $"√{requeste.number} = {result}");
				}

				return Ok(new SquareRootResponse { square = result });
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
					ErrorMessage = "Please provide a valid tracking ID"
				});
			}

			try
			{
				var entries = _journal.GetEntries(request.Id);
				return Ok(new JournalQueryResponse
				{
					operations = entries ?? new List<JournalEntry>()
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ErrorResponse
				{
					ErrorStatus = 500,
					ErrorMessage = $"An internal error occurred: {ex.Message}"
				});
			}
		}
	}
}
