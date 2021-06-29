using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AddAssetSummaryRequest
    {
        public Guid TargetId { get; set; }

        public TargetType TargetType { get; set; }

        public decimal TotalDwellingRent { get; set; }

        public decimal TotalNonDwellingRent { get; set; }

        public decimal TotalServiceCharges { get; set; }

        public decimal TotalRentalServiceCharge { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}
