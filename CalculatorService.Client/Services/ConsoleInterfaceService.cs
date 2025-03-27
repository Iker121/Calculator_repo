using CalculatorServerLibrary.Models;
using System;
using System.Threading.Tasks;

namespace CalculatorService.Client.Services
{
	public class ConsoleInterfaceService
	{
		private readonly CalculatorClientService _clientService;
		private string _trackingId;

		public ConsoleInterfaceService(CalculatorClientService clientService)
		{
			_clientService = clientService;
		}

		public async Task Run()
		{
			ShowWelcomeMessage();
			await SetupJournalTracking();

			while (true)
			{
				try
				{
					var option = ShowMenuAndGetOption();
					if (option == "7") break;

					await ProcessMenuOption(option);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"\n[Error] {ex.Message}");
					Console.WriteLine("Presione cualquier tecla para continuar...");
					Console.ReadKey();
				}
			}
		}

		private void ShowWelcomeMessage()
		{
			Console.Clear();
			Console.WriteLine("========================");
			Console.WriteLine("  CALCULATOR CLIENT");
			Console.WriteLine("========================");
		}

		private async Task SetupJournalTracking()
		{
			if (AskYesNoQuestion("¿Activar registro de operaciones (journal)?"))
			{
				Console.Write("Ingrese su ID de tracking: ");
				_trackingId = Console.ReadLine();
				_clientService.SetTrackingId(_trackingId);
			}
		}

		private string ShowMenuAndGetOption()
		{
			Console.WriteLine("\nOperaciones disponibles:");
			Console.WriteLine("1. Sumar");
			Console.WriteLine("2. Restar");
			Console.WriteLine("3. Multiplicar");
			Console.WriteLine("4. Dividir");
			Console.WriteLine("5. Raíz cuadrada");
			Console.WriteLine("6. Consultar journal");
			Console.WriteLine("7. Salir");
			Console.Write("Seleccione una opción: ");

			return Console.ReadLine()?.Trim();
		}

		private async Task ProcessMenuOption(string option)
		{
			switch (option)
			{
				case "1": await HandleAdd(); break;
				case "2": await HandleSubtract(); break;
				case "3": await HandleMultiply(); break;
				case "4": await HandleDivide(); break;
				case "5": await HandleSquareRoot(); break;
				case "6": await HandleJournalQuery(); break;
				default: Console.WriteLine("Opción no válida"); break;
			}
		}

		private async Task HandleAdd()
		{
			var numbers = GetNumbersFromUser("Ingrese números a sumar (separados por espacios): ");
			var result = await _clientService.Add(numbers);
			ShowResult($"Resultado de la suma: {result}");
		}

		private async Task HandleSubtract()
		{
			var (num1, num2) = GetTwoNumbersFromUser("minuendo", "sustraendo");
			var result = await _clientService.Subtract(num1, num2);
			ShowResult($"Resultado de la resta: {result}");
		}

		private async Task HandleMultiply()
		{
			var numbers = GetNumbersFromUser("Ingrese factores (separados por espacios): ");
			var result = await _clientService.Multiply(numbers);
			ShowResult($"Resultado de la multiplicación: {result}");
		}

		private async Task HandleDivide()
		{
			var (dividend, divisor) = GetTwoNumbersFromUser("dividendo", "divisor");
			var (quotient, remainder) = await _clientService.Divide(dividend, divisor);
			ShowResult($"Resultado: Cociente = {quotient}, Resto = {remainder}");
		}

		private async Task HandleSquareRoot()
		{
			var number = GetNumberFromUser("Ingrese un número: ");
			var result = await _clientService.SquareRoot(number);
			ShowResult($"Raíz cuadrada: {result}");
		}

		private async Task HandleJournalQuery()
		{
			if (string.IsNullOrEmpty(_trackingId))
			{
				Console.WriteLine("Journal no activado. Active el tracking primero.");
				return;
			}

			var journal = await _clientService.QueryJournal(_trackingId);
			Console.WriteLine("\n=== Historial de Operaciones ===");
			foreach (var entry in journal.operaciones)
			{
				Console.WriteLine($"{entry.Date:g}: {entry.operacion} - {entry.calculo}");
			}
		}

		// Helpers
		private double[] GetNumbersFromUser(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				var input = Console.ReadLine();
				if (TryParseNumbers(input, out var numbers)) return numbers;
				Console.WriteLine("Entrada inválida. Use números separados por espacios.");
			}
		}

		private (double, double) GetTwoNumbersFromUser(string name1, string name2)
		{
			return (
				GetNumberFromUser($"Ingrese {name1}: "),
				GetNumberFromUser($"Ingrese {name2}: ")
			);
		}

		private double GetNumberFromUser(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				if (double.TryParse(Console.ReadLine(), out var number)) return number;
				Console.WriteLine("Número inválido. Intente nuevamente.");
			}
		}

		private bool TryParseNumbers(string input, out double[] numbers)
		{
			numbers = Array.Empty<double>();
			if (string.IsNullOrWhiteSpace(input)) return false;

			var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			numbers = new double[parts.Length];

			for (int i = 0; i < parts.Length; i++)
			{
				if (!double.TryParse(parts[i], out numbers[i])) return false;
			}

			return true;
		}

		private void ShowResult(string message)
		{
			Console.WriteLine($"\n{message}");
			Console.WriteLine("\nPresione cualquier tecla para continuar...");
			Console.ReadKey();
		}

		private bool AskYesNoQuestion(string question)
		{
			Console.Write($"{question} (s/n): ");
			return Console.ReadLine()?.ToLower() == "s";
		}
	}
}