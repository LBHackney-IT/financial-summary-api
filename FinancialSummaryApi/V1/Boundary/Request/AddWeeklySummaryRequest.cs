using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AddWeeklySummaryRequest
    {
        /// <summary>
        /// Id of the appropriate asset
        /// </summary>
        /// <example>
        /// 95d9da98-c937-49a2-8d15-8683dc478e3b
        /// </example>
        [NonEmptyGuid]
        public Guid TargetId { get; set; }
        /// <example>
        /// 5
        /// </example>
        [Range(0, short.MaxValue)]
        public short PeriodNo { get; set; }
        /// <example>
        /// 2021
        /// </example>
        [Range(0, short.MaxValue)]
        public short FinancialYear { get; set; }
        /// <example>
        /// 10
        /// </example>
        [Range(0, short.MaxValue)]
        public short FinancialMonth { get; set; }
        /// <example>
        /// 2021-09-23T11:17:30.494+03:00
        /// </example>
        [RequiredDateTime]
        public DateTime WeekStartDate { get; set; }
        /// <example>
        /// 184.56
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal ChargedAmount { get; set; }
        /// <example>
        /// 84.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal PaidAmount { get; set; }
        /// <example>
        /// 158.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal BalanceAmount { get; set; }
        /// <example>
        /// 27.78
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal HousingBenefitAmount { get; set; }
    }
}
