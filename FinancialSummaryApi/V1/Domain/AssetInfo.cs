using System;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Domain
{
    public class AssetInfo
    {
        public Guid Id { get; set; }

        public string AssetName { get; set; }

        public List<string> AssetType { get; set; }
    }
}
