using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;

namespace FinancialSummaryApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static FinanceAssetSummaryDbEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<Entity>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static FinanceAssetSummaryDbEntity CreateDatabaseEntityFrom(Entity entity)
        {
            return new FinanceAssetSummaryDbEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
            };
        }
    }
}
