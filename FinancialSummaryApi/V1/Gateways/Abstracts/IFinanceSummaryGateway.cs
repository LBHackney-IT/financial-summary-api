using FinancialSummaryApi.V1.Domain;
using Hackney.Core.DynamoDb;
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
        Task<List<AssetSummary>> GetAllAssetSummaryAsync(Guid assetId, DateTime submitDate);

        Task<WeeklySummary> GetWeeklySummaryByIdAsync(Guid targetId, Guid id);
        Task<List<WeeklySummary>> GetAllWeeklySummaryAsync(Guid targetId, DateTime? startDate, DateTime? endDate);
        Task<PagedResult<Statement>> GetPagedStatementsAsync(Guid targetId, DateTime startDate, DateTime endDate, int pageSize, string paginationToken);
        Task<List<Statement>> GetStatementListAsync(Guid targetId, DateTime startDate, DateTime endDate);

        public Task AddRangeAsync(List<RentGroupSummary> groupSummaries);
        Task<Statement> GetStatementByIdAsync(Guid item, Guid targetId);
        public Task AddAsync(AssetSummary assetSummary);
        public Task AddAsync(WeeklySummary weeklySummary);
        public Task AddRangeAsync(List<Statement> statements);
    }
}
