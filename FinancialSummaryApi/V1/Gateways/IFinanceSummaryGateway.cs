using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways
{
    public interface IFinanceSummaryGateway
    {
        Task<RentGroupSummary> GetRentGroupSummaryByIdAsync(Guid id);
        Task<List<RentGroupSummary>> GetAllRentGroupSummaryAsync();

        Task<AssetSummary> GetAssetSummaryByIdAsync(Guid id);
        Task<List<AssetSummary>> GetAllAssetSummaryAsync();

        public Task AddAsync(RentGroupSummary groupSummary);
        public Task AddAsync(AssetSummary assetSummary);

        public Task UpdateAsync(RentGroupSummary groupSummary);
        public Task UpdateAsync(AssetSummary assetSummary);

        Task<string> GetAssetNameByTenureIdAsync(Guid tenureId);
        Task<string> GetAssetNameByAssetIdAsync(Guid assetId);
        Task<Guid> GetTenureIdByAssetIdAsync(Guid assetId);
    }
}
