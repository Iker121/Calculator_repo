namespace CalculatorService.Server.Services
{
	public interface ICalculatorService
	{
		double Add(double[] addends);
		double Substract(double minuend, double subtrahend);
		double Multiply(double[] factors);
		(double quotient, double remainder) Divide(double dividend, double divisor);
		double SquareRoot(double number);
	}
}
