using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    /// <summary>
    /// List of asset summaries
    /// </summary>
    public class AssetSummaryListResponse
    {
        public IEnumerable<AssetSummaryResponse> AssetSummaries { get; set; }
    }
}
