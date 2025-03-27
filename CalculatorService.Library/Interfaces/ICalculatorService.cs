namespace CalculatorServerLibrary.Interfaces
{
	public interface ICalculatorService
	{
		double Add(double[] sumandos);
		double Subtract(double minuendo, double substraendo);
		double Multiply(double[] factores);
		(double cociente, double resto) Divide(double dividendo, double divisor);
		double SquareRoot(double number);
	}
}