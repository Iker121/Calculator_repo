namespace CalculatorService.CalculatorService.Server.Services
{
    public interface ICalculator
    {
        double Add(double[] sumandos);
        double Subtract(double minuendo, double substraendo);
        double Multiply ( double[] factores);
        (double Cociente, double Resto) Divide (double dividendo , double divisor);
        double SquareRoot(double numero);
    }
}
