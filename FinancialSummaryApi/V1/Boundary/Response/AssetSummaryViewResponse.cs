using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class AssetSummaryViewResponse
    {
        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public short SummaryYear { get; set; }
        public ValuesType Type { get; set; }
        public OwnershipResponse Ownership { get; set; }
        public decimal TotalServiceCharges { get; set; }
    }
}
