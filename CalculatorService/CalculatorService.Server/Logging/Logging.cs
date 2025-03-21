using NLog.Extensions.Logging;

namespace CalculatorService.CalculatorService.Server.Logging
{
    public class Logging
    {
        public static void CongigureLogging(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddNLog();
        }
    }
}

