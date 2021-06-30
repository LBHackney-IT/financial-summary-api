using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;

namespace FinancialSummaryApi.V1.Factories
{
    public static class AssetInfoFactory
    {
        public static AssetInfo ToDomain(this AssetInfoDbEntity databaseEntity)
        {
            return databaseEntity == null ? null : new AssetInfo
            {
                Id = databaseEntity.Id,
                AssetName = databaseEntity.AssetId,
                AssetType = databaseEntity.AssetType
            };
        }
    }
}
