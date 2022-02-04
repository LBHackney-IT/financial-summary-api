using FinancialSummaryApi.V1.Exceptions.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace FinancialSummaryApi.V1.Exceptions.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelStateError> AllModelStateErrors(this ModelStateDictionary modelState)
        {
            if (modelState == null || !modelState.Any(m => m.Value.Errors.Count > 0))
            {
                return null;
            }

            var result = from ms in modelState
                         where ms.Value.Errors.Any()
                         let fieldKey = ms.Key
                         let errors = ms.Value.Errors
                         from error in errors
                         select new ModelStateError(fieldKey, error.ErrorMessage);

            return result;
        }
    }
}
