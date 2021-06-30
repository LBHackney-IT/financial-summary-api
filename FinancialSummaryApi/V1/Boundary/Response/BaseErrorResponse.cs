namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class BaseErrorResponse
    {
        public BaseErrorResponse() { }

        public BaseErrorResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
