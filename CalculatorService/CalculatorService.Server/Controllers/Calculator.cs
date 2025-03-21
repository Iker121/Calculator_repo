using CalculatorService.CalculatorService.Server.Services;
using CalculatorService.Server.Models;
using Microsoft.AspNetCore.Mvc;


namespace CalculatorService.Server.Controllers
{
    [ApiController]
    [Route("calculator")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator _calculator;
        private readonly IJournal _journal;
        private readonly ILogger<CalculatorController> _logger;

        // Inyectamos el logger en el constructor
        public CalculatorController(ICalculator calculator, IJournal journal, ILogger<CalculatorController> logger)
        {
            _calculator = calculator;
            _journal = journal;
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trakingId = null)
        {
            try
            {
                _logger.LogInformation("Iniciando operación de suma...");

                var result = _calculator.Add(request.sumandos);

                if (!string.IsNullOrEmpty(trakingId))
                {
                    _journal.AddEntry(trakingId, "Add", $"{string.Join(" + ", request.sumandos)} = {result}");
                }

                _logger.LogInformation("Operación de suma completada: {Result}", result);
                return Ok(new AddResponse { Sum = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la operación de suma");
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
            }
        }

        [HttpPost("Sub")]
        public IActionResult Subtract([FromBody] SubtractRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trakingId = null)
        {
            try
            {
                _logger.LogInformation("Iniciando operación de resta...");

                var result = _calculator.Subtract(request.Minuendo, request.Substraendo);

                if (!string.IsNullOrEmpty(trakingId))
                {
                    _journal.AddEntry(trakingId, "Subtract", $"{request.Minuendo} - {request.Substraendo} = {result}");
                }

                _logger.LogInformation("Operación de resta completada: {Result}", result);
                return Ok(new SubtractResponse { Diferencia = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la operación de resta");
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
            }
        }

        [HttpPost("Mul")]
        public IActionResult Multiply([FromBody] MultiplyRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trakingId = null)
        {
            try
            {
                _logger.LogInformation("Iniciando operación de multiplicación...");

                var result = _calculator.Multiply(request.Factores);

                if (!string.IsNullOrEmpty(trakingId))
                {
                    _journal.AddEntry(trakingId, "Multiply", $"{string.Join(" * ", request.Factores)} = {result}");
                }

                _logger.LogInformation("Operación de multiplicación completada: {Result}", result);
                return Ok(new MultiplyResponse { Producto = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la operación de multiplicación");
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
            }
        }

        [HttpPost("Div")]
        public IActionResult Divide([FromBody] DivideRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trakingId = null)
        {
            try
            {
                _logger.LogInformation("Iniciando operación de división...");

                var result = _calculator.Divide(request.Dividendo, request.Divisor);

                if (!string.IsNullOrEmpty(trakingId))
                {
                    _journal.AddEntry(trakingId, "Divide", $"{request.Dividendo} / {request.Divisor} = {result.Cociente} (Resto: {result.Resto})");
                }

                _logger.LogInformation("Operación de división completada: {Result}", result);
                return Ok(new DivideResponse { Cociente = result.Cociente, Resto = result.Resto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la operación de división");
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
            }
        }

        [HttpPost("SqRoot")]
        public IActionResult SquareRoot([FromBody] SquareRootRequest request, [FromHeader(Name = "X-Evi-Tracking-Id")] string trakingId = null)
        {
            try
            {
                _logger.LogInformation("Iniciando operación de raíz cuadrada...");

                var result = _calculator.SquareRoot(request.Numero);

                if (!string.IsNullOrEmpty(trakingId))
                {
                    _journal.AddEntry(trakingId, "Cuadrado", $"√{request.Numero} = {result}");
                }

                _logger.LogInformation("Operación de raíz cuadrada completada: {Result}", result);
                return Ok(new SquareRootResponse { Cuadrado = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la operación de raíz cuadrada");
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
            }
        }

        [HttpPost("Journal")]
        public IActionResult JournalQuery([FromBody] JournalQueryRequest request)
        {
            try
            {
                _logger.LogInformation("Consultando historial para Tracking-Id: {TrackingId}", request.Id);

                var entries = _journal.GetEntries(request.Id);
                return Ok(new JournalQueryResponse { Operaciones = entries });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar el historial");
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message, ErrorStatus = 400 });
            }
        }
    }
}