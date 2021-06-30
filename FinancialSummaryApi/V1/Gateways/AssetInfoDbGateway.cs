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
    public class AssetInfoDbGateway : IAssetInfoDbGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public AssetInfoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<string> GetAssetNameByAssetIdAsync(Guid assetId)
        {
            return (await GetAssetInfoAsync(assetId).ConfigureAwait(false))?.AssetName;
        }

        public async Task<AssetInfo> GetAssetInfoAsync(Guid assetId)
        {
            List<ScanCondition> scanToFindAsset = new List<ScanCondition>();

            scanToFindAsset.Add(new ScanCondition("Id", ScanOperator.Equal, assetId));

            List<AssetInfoDbEntity> assetInfo = await _dynamoDbContext.ScanAsync<AssetInfoDbEntity>(scanToFindAsset)
                .GetRemainingAsync()
                .ConfigureAwait(false);

            return assetInfo?.FirstOrDefault().ToDomain();
        }
    }
}
