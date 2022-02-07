using FinancialSummaryApi.V1.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Exceptions.CustomExceptions
{
    public class InvalidModelStateException : Exception
    {
        public int StatusCode { get; set; } = StatusCodes.Status422UnprocessableEntity;
        public IEnumerable<ModelStateError> Errors { get; set; }

        public InvalidModelStateException(IEnumerable<ModelStateError> errors, string message = "Invalid request object. Please correct the specified errors and try again.") : base(message)
        {
            Errors = errors;
        }
    }
}
