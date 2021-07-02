using System;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public static class ExceptionExtensions
    {
        private static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            if (e == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(msgs))
            {
                msgs = e.Message;
            }

            if (e.InnerException != null)
            {
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            }

            return msgs;
        }
    }
}
