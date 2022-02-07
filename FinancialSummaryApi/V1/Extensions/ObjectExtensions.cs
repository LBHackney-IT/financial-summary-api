using FinancialSummaryApi.V1.Exceptions.CustomExceptions;
using System.Net;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Extensions
{
    public static class ObjectExtensions
    {
        public static T EnsureExists<T>(this T obj, string errorMessage = "Entity not found", HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            if (obj is null)
            {
                throw new ApiException(errorMessage, statusCode);
            }

            return obj;
        }

        public static async Task<T> EnsureExistsAsync<T>(this Task<T> task, string errorMessage = "Entity not found", HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            var result = await task.ConfigureAwait(false);
            if (result is null)
            {
                throw new ApiException(errorMessage, statusCode);
            }

            return result;
        }
    }
}
