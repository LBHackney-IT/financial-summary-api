using AutoFixture;
using FinancialSummaryApi.V1.Infrastructure.Entities;

namespace FinancialSummaryApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static FinanceSummaryDbEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<FinanceSummaryDbEntity>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static FinanceSummaryDbEntity CreateDatabaseEntityFrom(FinanceSummaryDbEntity entity)
        {
            return new FinanceSummaryDbEntity
            {
                Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                SubmitDate = entity.SubmitDate,
                AssetSummaryData = entity.AssetSummaryData,
                RentGroupSummaryData = entity.RentGroupSummaryData
            };
        }
    }
}
