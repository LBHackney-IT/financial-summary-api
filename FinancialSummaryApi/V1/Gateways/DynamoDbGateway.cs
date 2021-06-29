using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways
{
    public class DynamoDbGateway : IFinanceSummaryGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public Task AddAsync(RentGroupSummary assetSummary)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(AssetSummary assetSummary)
        {
            await _dynamoDbContext.SaveAsync(assetSummary.ToDatabase()).ConfigureAwait(false);
        }

        public async Task<List<AssetSummary>> GetAllAssetSummaryAsync()
        {
            List<ScanCondition> scanConditions = new List<ScanCondition>();

            scanConditions.Add(new ScanCondition("SubmitDate", Amazon.DynamoDBv2.DocumentModel.ScanOperator.GreaterThanOrEqual, DateTime.UtcNow.Date.AddDays(-1)));

            List<FinanceSummaryDbEntity> data = await _dynamoDbContext.ScanAsync<FinanceSummaryDbEntity>(scanConditions).GetRemainingAsync().ConfigureAwait(false);

            return data.Select(s => s.ToAssetDomain()).ToList();
        }

        public Task<List<RentGroupSummary>> GetAllRentGroupSummaryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AssetSummary> GetAssetSummaryByIdAsync(Guid assetId)
        {
            List<ScanCondition> scanConditions = new List<ScanCondition>();

            scanConditions.Add(new ScanCondition("SubmitDate", Amazon.DynamoDBv2.DocumentModel.ScanOperator.GreaterThanOrEqual, DateTime.UtcNow.Date.AddDays(-1)));
            scanConditions.Add(new ScanCondition("TargetId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, assetId));

            List<FinanceSummaryDbEntity> data = await _dynamoDbContext.ScanAsync<FinanceSummaryDbEntity>(scanConditions).GetRemainingAsync().ConfigureAwait(false);

            return data.FirstOrDefault()?.ToAssetDomain();
        }

        public Task<string> GetAssetNameByTenureIdAsync(Guid tenureId)
        {
            return Task.FromResult("Mock Asset Name");
        }

        public Task<string> GetAssetNameByAssetIdAsync(Guid assetId)
        {
            return Task.FromResult("Mock Asset Name");
        }

        public Task<Guid> GetTenureIdByAssetIdAsync(Guid assetId)
        {
            return Task.FromResult(assetId);
        }

        public Task<RentGroupSummary> GetRentGroupSummaryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RentGroupSummary groupSummary)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AssetSummary assetSummary)
        {
            await _dynamoDbContext.SaveAsync<FinanceSummaryDbEntity>(assetSummary.ToDatabase()).ConfigureAwait(false);
        }
    }
}
