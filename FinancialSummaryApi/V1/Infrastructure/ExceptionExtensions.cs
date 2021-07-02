using System;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex?.Message + "; " + ex?.InnerException?.GetFullMessage();
        }
    }
}
