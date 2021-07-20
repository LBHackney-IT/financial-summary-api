namespace FinancialSummaryApi.V1.Boundary
{
    public class HealthCheckResponse
    {
        public HealthCheckResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <example>
        /// true
        /// </example>
        public bool Success { get; }

        /// <example>
        /// Service is avaliable
        /// </example>
        public string Message { get; }
    }
}
