using System.Collections.Generic;
using System.Linq;

namespace FinancialSummaryApi.V1.Exceptions.Models
{
    public class BaseErrorResponse
    {
        public BaseErrorResponse()
        { }

        public BaseErrorResponse(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details ?? string.Empty;
        }

        public BaseErrorResponse(int statusCode, IEnumerable<ModelStateError> modelErrors, string message = "Invalid request object. Please correct the specified errors and try again.")
        {
            if (modelErrors == null)
            {
                return;
            }

            var modelStateErrors = modelErrors.ToList();
            if (!modelStateErrors.Any())
            {
                Errors = null;
                return;
            }

            Message = message;
            StatusCode = statusCode;

            Errors = new List<ValidationError>();

            foreach (var res in modelStateErrors)
            {
                Errors.Add(new ValidationError
                {
                    Message = res.Message,
                    ControlId = res.Key,
                    Id = res.Key
                });
            }
        }

        /// <summary>
        /// Status code
        /// </summary>
        /// <example>
        /// 400
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
        /// The field PaidAmount must be from 0 to 79228162514264337593543950335
        /// </example>
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// List of errors
        /// </summary>
        public List<ValidationError> Errors { get; set; }
    }
}
