using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AddRentGroupSummaryRequest
    {
        [AllowedValues(TargetType.RentGroup)]
        public TargetType TargetType { get; set; }

        public string TargetDescription { get; set; }

        [Required]
        public string RentGroupName { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal TotalCharged { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal ChargedYTD { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalPaid { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal PaidYTD { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal ArrearsYTD { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalBalance { get; set; }

        [Required]
        public DateTime SubmitDate { get; set; }
    }
}
