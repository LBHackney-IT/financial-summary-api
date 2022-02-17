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
        public ValuesType ValuesType { get; set; }

        public string AssetName { get; set; }

        public decimal TotalDwellingRent { get; set; }

        public decimal TotalNonDwellingRent { get; set; }

        public decimal TotalServiceCharges { get; set; }

        public decimal TotalRentalServiceCharge { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpenditure { get; set; }

        public DateTime SubmitDate { get; set; }

        public short SummaryYear { get; set; }

        public int TotalLeaseholders { get; set; }

        public int TotalFreeholders { get; set; }

        public int TotalDwellings { get; set; }

        public int TotalBlocks { get; set; }
    }
}
