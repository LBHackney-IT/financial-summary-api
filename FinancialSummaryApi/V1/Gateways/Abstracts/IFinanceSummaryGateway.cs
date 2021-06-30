using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways.Abstracts
{
    public interface IFinanceSummaryGateway
    {
        Task<RentGroupSummary> GetRentGroupSummaryByNameAsync(string rentGroupName);
        Task<List<RentGroupSummary>> GetAllRentGroupSummaryAsync();

        Task<AssetSummary> GetAssetSummaryByIdAsync(Guid assetId);
        Task<List<AssetSummary>> GetAllAssetSummaryAsync();

        public Task AddAsync(RentGroupSummary groupSummary);
        public Task AddAsync(AssetSummary assetSummary);
    }
}
