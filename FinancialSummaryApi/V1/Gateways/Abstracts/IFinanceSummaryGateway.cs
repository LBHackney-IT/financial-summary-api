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
        public Task<StatementList> GetPagedStatementsAsync(Guid targetId, DateTime startDate, DateTime endDate, int pageSize, int pageNumber);

        public Task AddRangeAsync(List<RentGroupSummary> groupSummaries);
        public Task AddAsync(AssetSummary assetSummary);
        public Task AddAsync(WeeklySummary weeklySummary);
        public Task AddRangeAsync(List<Statement> statements);
    }
}
