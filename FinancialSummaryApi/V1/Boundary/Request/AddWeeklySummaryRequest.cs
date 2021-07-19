using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AddWeeklySummaryRequest
    {
        [NonEmptyGuid]
        public Guid TargetId { get; set; }
        
        [Range(0, short.MaxValue)]
        public short PeriodNo { get; set; }
       
        [Range(0, short.MaxValue)]
        public short FinancialYear { get; set; }
        
        [Range(0, short.MaxValue)]
        public short FinancialMonth { get; set; }

        [RequiredDateTime]
        public DateTime WeekStartDate { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal ChargedAmount { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal PaidAmount { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal BalanceAmount { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal HousingBenefitAmount { get; set; }
    }
}
