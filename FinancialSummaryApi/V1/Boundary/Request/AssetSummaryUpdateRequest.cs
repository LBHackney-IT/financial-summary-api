using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AssetSummaryUpdateRequest
    {
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

        public ValuesType ValuesType { get; set; }
    }
}
