using AutoFixture;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;

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
        public static WeeklySummaryDbEntity CreateWeeklySummaryDatabaseEntity()
        {
            var entity = new Fixture().Create<WeeklySummaryDbEntity>();

            return CreateWeeklySummaryCopy(entity);
        }

        public static WeeklySummaryDbEntity CreateWeeklySummaryCopy(WeeklySummaryDbEntity entity)
        {
            return new WeeklySummaryDbEntity
            {
                Id = entity.Id,
                TargetId = entity.TargetId,
                FinancialMonth = 7,
                FinancialYear = 2021,
                WeekStartDate = new DateTime(2021, 7, 2),
                PeriodNo = 1,
                BalanceAmount = 20,
                ChargedAmount = 150,
                PaidAmount = 120,
                HousingBenefitAmount = 10
            };
        }
    }
}
