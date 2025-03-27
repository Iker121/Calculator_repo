namespace CalculatorService.Server.Services
{
	public interface ICalculatorService
	{
		double Add(double[] sumando);
		double Substract(double minuendo, double substraendo);
		double Multiply(double[] factores);
		(double cociente, double resto) Divide(double dividendo, double divisor);
		double SquareRoot(double numero);
	}
}
