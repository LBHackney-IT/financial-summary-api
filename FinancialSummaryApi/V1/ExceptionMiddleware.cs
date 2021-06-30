using FinancialSummaryApi.V1.Boundary.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            #pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode code)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int) code;

            await response.WriteAsync(JsonConvert.SerializeObject(new BaseErrorResponse(message))).ConfigureAwait(false);
        }
    }
}
