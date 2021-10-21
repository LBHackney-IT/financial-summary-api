using System;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static Tuple<DateTime, DateTime> GetDayRange(this DateTime date)
           => new Tuple<DateTime, DateTime>(date.Date, date.Date.AddHours(23).AddMinutes(59));
    }
}
