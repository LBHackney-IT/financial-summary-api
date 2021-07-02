using AutoFixture;
using FinancialSummaryApi.V1.Infrastructure.Entities;

namespace FinancialSummaryApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static FinanceSummaryDbEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<FinanceSummaryDbEntity>();

            return CreateCopy(entity);
        }

        public static FinanceSummaryDbEntity CreateCopy(FinanceSummaryDbEntity entity)
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
