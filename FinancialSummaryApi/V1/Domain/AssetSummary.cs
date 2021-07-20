using System;

namespace FinancialSummaryApi.V1.Domain
{
    /// <summary>
    /// Model that describe summary info about some estate
    /// </summary>
    public class AssetSummary
    {
        public Guid Id { get; set; }

        public Guid TargetId { get; set; }

        public TargetType TargetType { get; set; }

        public string AssetName { get; set; }

        public decimal TotalDwellingRent { get; set; }

        public decimal TotalNonDwellingRent { get; set; }

        public decimal TotalServiceCharges { get; set; }

        public decimal TotalRentalServiceCharge { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}
