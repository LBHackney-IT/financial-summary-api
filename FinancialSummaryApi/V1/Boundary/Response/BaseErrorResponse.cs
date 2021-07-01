namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class BaseErrorResponse
    {
        public BaseErrorResponse() { }

        public BaseErrorResponse(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        /// <summary>
        /// Status code
        /// </summary>
        /// <example>
        /// 400, 404, 500
        /// </example>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        /// <example>
        /// Model cannot be null
        /// </example>>
        public string Message { get; set; }

        /// <summary>
        /// Stack Trace of Exception
        /// </summary>
        /// <example>
        /// at FinancialSummaryApi.V1.UseCase.AddAssetSummaryUseCase.ExecuteAsync(AddAssetSummaryRequest assetSummary)
        /// </example>
        public string Details { get; set; }
    }
}
