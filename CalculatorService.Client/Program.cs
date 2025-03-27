using CalculatorService.Client.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("CALCULATOR");
			Console.WriteLine("===================");

			var apiClient = new CalculatorApiClient("http://localhost:5000");

			if (AskYesNoQuestion("Enable operation tracking?"))
			{
				Console.Write("Enter your tracking ID: ");
				var trackingId = Console.ReadLine();
				if (!string.IsNullOrWhiteSpace(trackingId))
				{
					apiClient.SetTrackingId(trackingId);
					Console.WriteLine();
					try
					{
						await apiClient.AddAsync(new double[] { 1, 2 });
					}
					catch
					{
						Console.WriteLine("Connection failed");
					}
				}
			}

			while (true)
			{
				try
				{
					Console.WriteLine("\nAvailable operations:");
					Console.WriteLine("1. Add");
					Console.WriteLine("2. Subtract");
					Console.WriteLine("3. Multiply");
					Console.WriteLine("4. Divide");
					Console.WriteLine("5. Square root");
					Console.WriteLine("6. Query history");
					Console.WriteLine("7. Exit");
					Console.Write("Select an option: ");

					var option = Console.ReadLine();

					switch (option)
					{
						case "1":
							await HandleOperation(apiClient.AddAsync, "Enter two numbers separated by space: ", minNumbers: 2);
							break;
						case "2":
							await HandleBinaryOperation(apiClient.SubstractAsync, "Minuend: ", "Subtrahend: ");
							break;
						case "3":
							await HandleOperation(apiClient.MultiplyAsync, "Enter two numbers separated by space: ", minNumbers: 2);
							break;
						case "4":
							await HandleDivision(apiClient);
							break;
						case "5":
							await HandleUnaryOperation(apiClient.SquareRootAsync, "Enter a number greater than 0: ");
							break;
						case "6":
							await QueryJournal(apiClient);
							break;
						case "7":
							return;
						default:
							Console.WriteLine("Invalid option");
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"\nError: {ex.Message}");
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
				}
			}
		}

		static bool AskYesNoQuestion(string question)
		{
			Console.Write($"{question} (y/n): ");
			return Console.ReadLine()?.Trim().ToLower() == "y";
		}

		static async Task HandleOperation(Func<double[], Task<double>> operation, string prompt, int minNumbers = 1)
		{
			try
			{
				Console.Write(prompt);
				var input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
					throw new ArgumentException("Please enter values");

				var numbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(y =>
					{
						if (!double.TryParse(y, out var num))
							throw new FormatException($"Invalid value: '{y}'");
						return num;
					})
					.ToArray();

				if (numbers.Length < minNumbers)
					throw new FormatException($"At least {minNumbers} numbers required");

				var result = await operation(numbers);
				Console.WriteLine($"\nResult: {result}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Only valid numbers allowed");
			}
		}

		static async Task HandleBinaryOperation(Func<double, double, Task<double>> operation, string prompt1, string prompt2)
		{
			try
			{
				Console.Write(prompt1);
				if (!double.TryParse(Console.ReadLine(), out var num1))
					throw new FormatException("First number is invalid");

				Console.Write(prompt2);
				if (!double.TryParse(Console.ReadLine(), out var num2))
					throw new FormatException("Second number is invalid");

				var result = await operation(num1, num2);
				Console.WriteLine($"\nResult: {result}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Only valid numbers allowed");
			}
		}

		static async Task HandleUnaryOperation(Func<double, Task<double>> operation, string prompt)
		{
			try
			{
				Console.Write(prompt);
				if (!double.TryParse(Console.ReadLine(), out var num))
					throw new FormatException("Invalid number");

				var result = await operation(num);
				Console.WriteLine($"\nResult: {result}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Only valid numbers allowed");
			}
		}

		static async Task HandleDivision(CalculatorApiClient client)
		{
			try
			{
				Console.Write("Dividend: ");
				if (!double.TryParse(Console.ReadLine(), out var dividend))
					throw new FormatException("Invalid dividend");

				Console.Write("Divisor (must be greater than 0): ");
				if (!double.TryParse(Console.ReadLine(), out var divisor) || divisor == 0)
					throw new FormatException("Divisor must be different from 0");

				var (quotient, remainder) = await client.DivideAsync(dividend, divisor);
				Console.WriteLine($"\nResult: Quotient = {quotient}, Remainder = {remainder}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Only valid numbers allowed");
			}
		}

		static async Task QueryJournal(CalculatorApiClient client)
		{
			try
			{
				Console.Write("Enter tracking ID: ");
				var trackingId = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(trackingId))
					throw new ArgumentException("Tracking ID is required");

				var journal = await client.QueryJournalAsync(trackingId);

				if (journal.operations == null || journal.operations.Count == 0)
				{
					Console.WriteLine($"No operations found for ID: {trackingId}");
					Console.WriteLine("Possible reasons:");
					Console.WriteLine("- Incorrect ID");
					Console.WriteLine("- No operations performed with this ID");
					Console.WriteLine("- Server was restarted and temporary data was lost");
					return;
				}

				Console.WriteLine("\nOPERATION HISTORY");
				Console.WriteLine("=======================");
				foreach (var entry in journal.operations.OrderByDescending(e => e.Date))
				{
					Console.WriteLine($"{entry.Date:HH:mm:ss}] {entry.operation}: {entry.calculation}");
				}
			}
			catch (HttpRequestException ex) when (ex.Message.Contains("400"))
			{
				Console.WriteLine("Error: Server rejected the request. Please check the ID format");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unexpected error: {ex.Message}");
			}
		}
	}
}