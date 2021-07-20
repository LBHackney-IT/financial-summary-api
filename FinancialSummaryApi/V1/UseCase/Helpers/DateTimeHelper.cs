using System;

namespace FinancialSummaryApi.V1.UseCase.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime? CheckAndConvertDateTime(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            else
            {
                DateTime result;
                if (!DateTime.TryParse(input, out result))
                {
                    return null;
                }
                return result;
            }
        }
    }
}
