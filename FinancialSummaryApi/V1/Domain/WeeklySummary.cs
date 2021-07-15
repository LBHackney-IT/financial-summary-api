using System;

namespace FinancialSummaryApi.V1.Domain
{
    public class WeeklySummary
    {
        public Guid Id { get; set; }

        public Guid TargetId { get; set; }

        public short PeriodNo { get; set; }

        public short FinancialYear { get; set; }

        public short FinancialMonth { get; set; }

        public DateTime WeekStartDate { get; set; }

        public decimal ChargedAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

        public decimal HousingBenefitAmount { get; set; }
    }
}
