using FinancialSummaryApi.V1.Exceptions.Models;
using System;
using System.Net;

namespace FinancialSummaryApi.V1.Exceptions.CustomExceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ValidationErrorCollection Errors { get; set; }

        public string Details { get; set; }

        public ApiException(string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            ValidationErrorCollection errors = null,
            string details = null) :
            base(message)
        {
            StatusCode = (int) statusCode;
            Errors = errors;
            Details = details;
        }
    }
}
