namespace CalculatorService.Server.Services
{
	public class CalculatorService : ICalculatorService
	{
		public double Add(double[] sumandos)
		{
			if (sumandos == null || sumandos.Length < 2)
				throw new ArgumentException("Se necesitan minimo 2 numeros");
			return sumandos.Sum();
		}
		public double Substract(double minuendo, double substraendo )
		{
			return minuendo - substraendo;
		}
		public double Multiply(double[] factores)
		{
			if (factores == null || factores.Length < 2)
				throw new ArgumentException("Se necesitan minimo 2 numeros");
			return factores.Aggregate(1.0, (acc, val)=> acc * val);
		}
		public (double cociente, double resto) Divide (double dividendo, double divisor)
		{
			if (divisor == 0) 
				throw new DivideByZeroException("No puede ser 0");

			return (dividendo / divisor, dividendo % divisor);
		}
		public double SquareRoot (double numero)
		{
			if (numero < 0) 
				throw new ArgumentException("No puede ser negativo");
			return Math.Sqrt(numero);
		}
	}
}
