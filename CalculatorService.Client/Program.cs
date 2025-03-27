using CalculatorService.Client.Services;


namespace CalculatorClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("CALCULATOR");
			Console.WriteLine("===================");

			var apiClient = new CalculatorApiClient("http://localhost:5000");

			if (AskYesNoQuestion("¿Activar tracking de operaciones?"))
			{
				Console.Write("Ingrese su ID de tracking: ");
				var trackingId = Console.ReadLine();
				//apiClient.SetTrackingId(trackingId);
				//Console.WriteLine("Realizando una operación de prueba...");
				//await apiClient.AddAsync(new double[] { 1, 2 });
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
						Console.WriteLine("No se pudo conectar");
					}
				}
			}
			while (true)
			{
				try
				{
					Console.WriteLine("\nOperaciones disponibles:");
					Console.WriteLine("1. Sumar");
					Console.WriteLine("2. Restar");
					Console.WriteLine("3. Multiplicar");
					Console.WriteLine("4. Dividir");
					Console.WriteLine("5. Raiz cuadrada");
					Console.WriteLine("6. Consultar historial");
					Console.WriteLine("7. Salir");
					Console.Write("Seleccione una opción: ");

					var option = Console.ReadLine();

					switch (option)
					{
						case "1":
							await HandleOperation(apiClient.AddAsync, "Inserta dos numero, separados por espacio: ", minNumbers : 2);
							break;
						case "2":
							await HandleBinaryOperation(apiClient.SubstractAsync, "Minuendo: ", "Sustraendo: ");
							break;
						case "3":
							await HandleOperation(apiClient.MultiplyAsync, "Inserta dos numero, separados por espacio: ", minNumbers: 2);
							break;
						case "4":
							await HandleDivision(apiClient);
							break;
						case "5":
							await HandleUnaryOperation(apiClient.SquareRootAsync, "Ingrese un numero mayor a que 0: ");
							break;
						case "6":
							await QueryJournal(apiClient);
							break;
						case "7":
							return;
						default:
							Console.WriteLine("Opción no válida");
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"\nError: {ex.Message}");
					Console.WriteLine("Presione cualquier tecla para continuar...");
					Console.ReadKey();
				}
			}
		}
		static bool AskYesNoQuestion(string question)
		{
			Console.Write($"{question} (s/n): ");
			return Console.ReadLine()?.Trim().ToLower() == "s";
		}

		static async Task HandleOperation(Func<double[], Task<double>> operation, string prompt, int minNumbers = 1)
		{
			try
			{
				Console.Write(prompt);
				var input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
					throw new ArgumentException("Ingresa los valores");
				var numeros = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					.Select(s =>
					{
						if (!double.TryParse(s, out var num))
							throw new FormatException($"Valor no valido:'{s}'");
						return num;
					})
					.ToArray();
				if (numeros.Length < minNumbers)
					throw new FormatException($"Se necesitan al menos {minNumbers} numeros ");
				var result = await operation(numeros);
				Console.WriteLine($"\nResultado: {result}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Solo numeros validos");
			}
		}

		static async Task HandleBinaryOperation(Func<double, double, Task<double>> operation, string prompt1, string prompt2)
		{
			try
			{
				Console.Write(prompt1);
				if (!double.TryParse(Console.ReadLine(), out var num1))
					throw new FormatException("Primer numero no valido");

				Console.Write(prompt2);
				if (!double.TryParse(Console.ReadLine(), out var num2))
					throw new FormatException("Segundo numero no valido");
				var result = await operation(num1, num2);
				Console.WriteLine($"\nResultado: {result}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Solo numeros validos");
			}
		}

		static async Task HandleUnaryOperation(Func<double, Task<double>> operation, string prompt)
		{
			try
			{
				Console.Write(prompt);
				if (!double.TryParse(Console.ReadLine(), out var num))
					throw new FormatException("Numero no valido");
				var result = await operation(num);
				Console.WriteLine($"\nResultado: {result}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Solo numeros validos");
			}
		}

		static async Task HandleDivision(CalculatorApiClient client)
		{
			try
			{
				Console.Write("Dividiendo: ");
				if (!double.TryParse(Console.ReadLine(), out var dividendo))
					throw new FormatException("Dividendo no valido");

				Console.Write("Divisor mayor que 0: ");
				if (!double.TryParse(Console.ReadLine(), out var divisor) || divisor == 0)
					throw new FormatException("Divisor diferente a 0");

				var (cociente, resto) = await client.DivideAsync(dividendo, divisor);
				Console.WriteLine($"\nResultado: Cociente = {cociente}, Resto = {resto}");
			}
			catch (FormatException)
			{
				throw new ArgumentException("Solo numeros validos");
			}
		}
		static async Task QueryJournal(CalculatorApiClient client)
		{
			try
			{
				Console.Write("Ingrese ID de tracking: ");
				var trackingId = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(trackingId))
					throw new ArgumentException("Tiene que ingresa tracking id");
				

				var journal = await client.QueryJournalAsync(trackingId);

				if (journal.operaciones == null || journal.operaciones.Count == 0)
				{
					Console.WriteLine($"No se encontraron operaciones para el ID: {trackingId}");
					Console.WriteLine("Posibles causas:");
					Console.WriteLine("- El ID no es correcto");
					Console.WriteLine("- No se hicieron operaciones con este ID");
					Console.WriteLine("- El servidor se ha reiniciado y se han perdido datos termporales ");
					return;
				}

				Console.WriteLine("\nHISTORIAL DE OPERACIONES");
				Console.WriteLine("=======================");
				foreach (var entry in journal.operaciones.OrderByDescending(e => e.Date))
				{
					Console.WriteLine($"{entry.Date:HH:mm:ss}] {entry.operacion}: {entry.calculo}");
				}
			}
			catch (HttpRequestException ex) when (ex.Message.Contains("400"))
			{
				Console.WriteLine("Error: El servidor rechazo la solicitud. Revisa el formato del ID");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error inesperado: {ex.Message}");
			}
		}
	}
}