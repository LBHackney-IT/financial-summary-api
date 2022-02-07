using FinancialSummaryApi.V1.Exceptions.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace FinancialSummaryApi.V1.Exceptions.CustomExceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public List<ValidationError> Errors { get; set; }

        public string Details { get; set; }

        public ApiException(string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            string details = null,
            List<ValidationError> errors = null) :
            base(message)
        {
            StatusCode = (int) statusCode;
            Errors = errors;
            Details = details;
        }
    }
}
