using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways.Abstracts
{
    public interface IFinanceSummaryGateway
    {
        Task<RentGroupSummary> GetRentGroupSummaryByNameAsync(string rentGroupName, DateTime submitDate);
        Task<List<RentGroupSummary>> GetAllRentGroupSummaryAsync(DateTime submitDate);

        Task<AssetSummary> GetAssetSummaryByIdAsync(Guid assetId, DateTime submitDate);
        Task<List<AssetSummary>> GetAllAssetSummaryAsync(DateTime submitDate);

        Task<WeeklySummary> GetWeeklySummaryByIdAsync(Guid id);
        Task<List<WeeklySummary>> GetAllWeeklySummaryAsync(Guid targetId, DateTime? startDate, DateTime? endDate);
        Task<GetStatementListResponse> GetStatementsListAsync(Guid targetId, GetStatementListRequest request);

        public Task AddAsync(RentGroupSummary groupSummary);
        public Task AddAsync(AssetSummary assetSummary);
        public Task AddAsync(WeeklySummary weeklySummary);
        public Task AddAsync(Statement statement);
    }
}
