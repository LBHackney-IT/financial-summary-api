using System;

namespace FinancialSummaryApi.V1.Domain
{
    /// <summary>
    /// Model that describe summary info about some statement 
    /// </summary>
    public class Statement
    {
        public Guid Id { get; set; }

        public Guid TargetId { get; set; }

        public TargetType TargetType { get; set; }

        public DateTime StatementPeriodEndDate { get; set; }

        public string RentAccountNumber { get; set; }

        public string Address { get; set; }

        public StatementType StatementType { get; set; }

        public decimal ChargedAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal HousingBenefitAmount { get; set; }

        public decimal StartBalance { get; set; }

        public decimal FinishBalance { get; set; }
    }
}
