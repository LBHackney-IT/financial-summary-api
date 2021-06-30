using FinancialSummaryApi.V1.Domain;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways.Abstracts
{
    public interface ITenureInfoDbGateway
    {
        Task<Guid> GetTenureIdByAssetIdAsync(Guid assetId);

        Task<TenureInfo> GetTenureInfoAsync(Guid tenureId);
    }
}
