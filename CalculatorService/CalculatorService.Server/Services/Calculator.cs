namespace CalculatorService.CalculatorService.Server.Services
{
    public class Calculator : ICalculator
    {
         public double Add(double[] sumandos)  => sumandos.Sum();

        public double Subtract (double minuendo, double substraendo) => minuendo - substraendo;

        public double Multiply ( double[] factores ) => factores.Aggregate (1.0, (num1, num2)=> num1 * num2);

        public (double Cociente, double Resto) Divide(double dividendo, double divisor)
        {
            if (divisor == 0) throw new DivideByZeroException("No puede ser 0");

            return (dividendo / divisor, dividendo % divisor);
        }
        public double SquareRoot (double numero)
        {
            if (numero < 0) throw new ArgumentException("No puede ser negativo");
            return Math.Sqrt(numero);
        }
    }
}
