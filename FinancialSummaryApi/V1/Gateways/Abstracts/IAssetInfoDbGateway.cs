using FinancialSummaryApi.V1.Domain;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways.Abstracts
{
    public interface IAssetInfoDbGateway
    {
        Task<string> GetAssetNameByAssetIdAsync(Guid assetId);

        Task<AssetInfo> GetAssetInfoAsync(Guid assetId);
    }
}
