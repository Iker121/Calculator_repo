namespace CalculatorService.Server.Models
{
    public class AddRequest
    {
        public double[] sumandos { get; set; }
    }
    public class AddResponse
    {
        public double Sum { get; set; }
    }
    public class SubtractRequest
    {
        public double Minuendo { get; set; }
        public double Substraendo { get; set; }
    }

    public class SubtractResponse
    {
        public double Diferencia { get; set; }
    }
    public class MultiplyRequest
    {
        public double[] Factores { get; set; }
    }

    public class MultiplyResponse
    {
        public double Producto { get; set; }
    }
    public class DivideRequest
    {
        public double Dividendo { get; set; }
        public double Divisor { get; set; }
    }

    public class DivideResponse
    {
        public double Cociente { get; set; }
        public double Resto { get; set; }
    }
    public class SquareRootRequest
    {
        public double Numero { get; set; }
    }

    public class SquareRootResponse
    {
        public double Cuadrado { get; set; }
    }
    public class JournalQueryRequest
    {
        public string Id { get; set; } 
    }

    public class JournalQueryResponse
    {
        public List<JournalEntry> Operaciones { get; set; }
    }

    public class JournalEntry
    {
        public string Operaciones { get; set; } 
        public string Calculo { get; set; }
        public DateTime Date { get; set; } 
    }
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}