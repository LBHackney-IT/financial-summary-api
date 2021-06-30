using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways
{
    public class TenureInfoDbGateway : ITenureInfoDbGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public TenureInfoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public Task<Guid> GetTenureIdByAssetIdAsync(Guid assetId)
        {
            return Task.FromResult(assetId);
            //AssetInfoDbEntity assetInfo = await GetAssetInfoAsync(assetId).ConfigureAwait(false);

            //List<ScanCondition> scanToFindTenure = new List<ScanCondition>();

            //scanToFindTenure.Add(new ScanCondition("Id", ScanOperator.Equal, tenureId));

            //List<TenureInfoDbEntity> tenureInfo = await _dynamoDbContext.ScanAsync<TenureInfoDbEntity>(scanToFindTenure)
            //    .GetRemainingAsync()
            //    .ConfigureAwait(false);
        }

        public async Task<TenureInfo> GetTenureInfoAsync(Guid tenureId)
        {
            List<ScanCondition> scanToFindTenure = new List<ScanCondition>();

            scanToFindTenure.Add(new ScanCondition("Id", ScanOperator.Equal, tenureId));

            List<TenureInfoDbEntity> tenureInfo = await _dynamoDbContext.ScanAsync<TenureInfoDbEntity>(scanToFindTenure)
                .GetRemainingAsync()
                .ConfigureAwait(false);

            if (tenureInfo == null || !tenureInfo.Any() || tenureInfo.First().TenuredAsset == null)
            {
                // ToDo: how to handle this case?
                return null;
            }

            return tenureInfo.FirstOrDefault().ToDomain();
        }
    }
}
