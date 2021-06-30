using System;

namespace FinancialSummaryApi.V1.Domain
{
    public class TenureInfo
    {
        public Guid Id { get; set; }

        public string PaymentReference { get; set; }

        public TenureAsset TenuredAsset { get; set; }
    }
}
