namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class BaseErrorResponse
    {
        public BaseErrorResponse() { }

        public BaseErrorResponse(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Error message
        /// </summary>
        /// <example>
        /// Model cannot be null
        /// </example>>
        public string Message { get; set; }
    }
}
