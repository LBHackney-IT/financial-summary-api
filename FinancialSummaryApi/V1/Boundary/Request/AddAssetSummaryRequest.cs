using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AddAssetSummaryRequest
    {
        [Required]
        public Guid TargetId { get; set; }

        [AllowedValues(TargetType.Estate, TargetType.Block)]
        public TargetType TargetType { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal TotalDwellingRent { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalNonDwellingRent { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalServiceCharges { get; set; }

        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalRentalServiceCharge { get; set; }

        [Required]
        public DateTime SubmitDate { get; set; }
    }
}
