using System;

namespace FinancialSummaryApi.V1.Domain
{
    /// <summary>
    /// Model that describe summary info about some rent group
    /// </summary>
    public class RentGroupSummary
    {
        public Guid Id { get; set; }

        // ToDo how to validate type to be always rent group 
        public TargetType TargetType { get; set; }

        public string TargetDescription { get; set; }

        public string RentGroupName { get; set; }

        public decimal TotalCharged { get; set; }

        public decimal ChargedYTD { get; set; }

        public decimal TotalPaid { get; set; }

        public decimal PaidYTD { get; set; }

        public decimal ArrearsYTD { get; set; }

        public decimal TotalBalance { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}
