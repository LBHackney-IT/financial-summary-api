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
        Task<AssetSummary> GetAssetSummaryByIdAndYearAsync(Guid assetId, short summaryYear, ValuesType valuesType);
        Task<List<AssetSummary>> GetAllAssetSummaryAsync(Guid assetId, DateTime? submitDate);

        Task<PagedResult<Statement>> GetPagedStatementsAsync(Guid targetId, DateTime startDate, DateTime endDate, int pageSize, string paginationToken);
        Task<List<Statement>> GetStatementListAsync(Guid targetId, DateTime? startDate, DateTime? endDate);

        public Task AddRangeAsync(List<RentGroupSummary> groupSummaries);
        Task<Statement> GetStatementByIdAsync(Guid item, Guid targetId);
        public Task AddAsync(AssetSummary assetSummary);

        Task<bool> AddBatchAsync(List<AssetSummary> assetSummaries);
        Task UpdateAsync(AssetSummary assetSummary);
        public Task AddRangeAsync(List<Statement> statements);
    }
}
