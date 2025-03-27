namespace CalculatorService.Server.Services
{
	public class CalculatorService : ICalculatorService
	{
		public double Add(double[] addends)
		{
			if (addends == null || addends.Length < 2)
				throw new ArgumentException("At least 2 numbers are required");
			return addends.Sum();
		}

		public double Substract(double minuend, double subtrahend)
		{
			return minuend - subtrahend;
		}

		public double Multiply(double[] factors)
		{
			if (factors == null || factors.Length < 2)
				throw new ArgumentException("At least 2 numbers are required");
			return factors.Aggregate(1.0, (acc, val) => acc * val);
		}

		public (double quotient, double remainder) Divide(double dividend, double divisor)
		{
			if (divisor == 0)
				throw new DivideByZeroException("Cannot be 0");

			return (dividend / divisor, dividend % divisor);
		}

		public double SquareRoot(double number)
		{
			if (number < 0)
				throw new ArgumentException("Cannot be negative");
			return Math.Sqrt(number);
		}
	}
}