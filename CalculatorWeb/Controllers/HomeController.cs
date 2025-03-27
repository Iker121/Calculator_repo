using CalculatorWeb.Services;
using Microsoft.AspNetCore.Mvc;
using CalculatorWeb.Models;
namespace CalculatorWeb.Controllers
{
	public class HomeController(ICalculatorService calculatorService) : Controller
	{
        private readonly ICalculatorService _calculatorService = calculatorService;

		public IActionResult Index()
        {
            return View(new CalculatorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(CalculatorViewModel models)
        {
            try
            {
                var numbers = models.Input.Split(',')
                    .Select(x => double.Parse(x.Trim()))
                    .ToArray();

                switch (models.Operation)
                {
                    case "add":
                        models.Result = await _calculatorService.AddAsync(numbers);
                        models.History.Add($"{string.Join(" + ", numbers)} = {models.Result}");
                        break;
                    case "subtract" when numbers.Length == 2:
                        models.Result = await _calculatorService.SubtractAsync(numbers[0], numbers[1]);
                        models.History.Add($"{numbers[0]} - {numbers[1]} = {models.Result}");
                        break;
                    // Implementar otros casos...
                    default:
                        models.ErrorMessage = "Operación no válida";
                        break;
                }
            }
            catch (Exception ex)
            {
                models.ErrorMessage = ex.Message;
            }

            return View("Index", models);
        }
    }
}