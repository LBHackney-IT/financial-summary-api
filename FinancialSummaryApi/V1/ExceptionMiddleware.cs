using FinancialSummaryApi.V1.Exceptions.CustomExceptions;
using FinancialSummaryApi.V1.Exceptions.Models;
using FinancialSummaryApi.V1.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debugger.Break(); // Break if debugger is attached

                var errorMessage = ex.GetFullMessage();
                BaseErrorResponse errorResponse;

                switch (ex)
                {
                    case ApiException apiException:
                        errorResponse = new BaseErrorResponse(apiException.StatusCode, apiException.Message, apiException.Details) { Errors = apiException.Errors };
                        break;

                    case InvalidModelStateException invalidModelStateException:
                        errorResponse = new BaseErrorResponse(invalidModelStateException.Errors, invalidModelStateException.Message);
                        break;

                    case ArgumentNullException _:
                        errorResponse = new BaseErrorResponse(StatusCodes.Status400BadRequest, errorMessage, string.Empty);
                        break;

                    case ArgumentException _:
                        errorResponse = new BaseErrorResponse(StatusCodes.Status400BadRequest, errorMessage, string.Empty);
                        break;

                    case KeyNotFoundException _:
                        errorResponse = new BaseErrorResponse(StatusCodes.Status400BadRequest, errorMessage, string.Empty);
                        break;

                    case UnauthorizedAccessException _:
                        errorResponse = new BaseErrorResponse(StatusCodes.Status401Unauthorized, "Request unauthorized", string.Empty);
                        break;

                    default:
                        // unhandled error
                        _logger.LogError(ex, ex.StackTrace);
                        var details = _env.IsDevelopment() ? ex.StackTrace : string.Empty;
                        errorResponse = new BaseErrorResponse(StatusCodes.Status500InternalServerError, $"An unhandled error occurred: {errorMessage}", details);
                        break;
                }

                var response = context.Response;
                response.ContentType = "application/problem+json";
                response.StatusCode = errorResponse.StatusCode;
                await response
                    .WriteAsync(JsonConvert.SerializeObject(errorResponse))
                    .ConfigureAwait(false);
            }
        }
    }
}
