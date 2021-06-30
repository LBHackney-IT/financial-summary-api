using System;

namespace FinancialSummaryApi.V1.Domain
{
    public class TenureAsset
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string FullAddress { get; set; }
    }
}
