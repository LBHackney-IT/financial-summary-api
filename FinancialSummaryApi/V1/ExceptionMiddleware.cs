using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code)
        {
            _logger.LogError(ex, ex.StackTrace);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int) code;
            var allMessageText = ex.GetFullMessage();

            var details = _env.IsDevelopment() && code == HttpStatusCode.InternalServerError
                ? ex.StackTrace?.ToString() :
                  string.Empty;

            await response.WriteAsync(JsonConvert.SerializeObject(new BaseErrorResponse((int) code, allMessageText, details)))
                    .ConfigureAwait(false);
        }
    }
}
