using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;

namespace FinancialSummaryApi.V1.Factories
{
    public static class WeeklySummaryFactory
    {
        public static WeeklySummary ToWeeklySummaryDomain(this WeeklySummaryDbEntity databaseEntity)
        {
            if (databaseEntity == null)
            {
                return null;
            }
            return new WeeklySummary
            {
                Id = databaseEntity.Id,
                TargetId = databaseEntity.TargetId,
                BalanceAmount = databaseEntity.BalanceAmount,
                ChargedAmount = databaseEntity.ChargedAmount,
                FinancialMonth = databaseEntity.FinancialMonth,
                FinancialYear = databaseEntity.FinancialYear,
                HousingBenefitAmount = databaseEntity.HousingBenefitAmount,
                PaidAmount = databaseEntity.PaidAmount,
                PeriodNo = databaseEntity.PeriodNo,
                WeekStartDate = databaseEntity.WeekStartDate
            };
        }

        public static WeeklySummaryDbEntity ToDatabase(this WeeklySummary entity)
        {
            return entity == null ? null : new WeeklySummaryDbEntity
            {
                Id = entity.Id,
                TargetId = entity.TargetId,
                BalanceAmount = entity.BalanceAmount,
                ChargedAmount = entity.ChargedAmount,
                FinancialMonth = entity.FinancialMonth,
                FinancialYear = entity.FinancialYear,
                HousingBenefitAmount = entity.HousingBenefitAmount,
                PaidAmount = entity.PaidAmount,
                PeriodNo = entity.PeriodNo,
                WeekStartDate = entity.WeekStartDate
            };
        }

        public static WeeklySummary ToDomain(this AddWeeklySummaryRequest requestModel)
        {
            return requestModel == null ? null : new WeeklySummary
            {
                TargetId = requestModel.TargetId,
                BalanceAmount = requestModel.BalanceAmount,
                ChargedAmount = requestModel.ChargedAmount,
                FinancialMonth = requestModel.FinancialMonth,
                FinancialYear = requestModel.FinancialYear,
                HousingBenefitAmount = requestModel.HousingBenefitAmount,
                PaidAmount = requestModel.PaidAmount,
                PeriodNo = requestModel.PeriodNo,
                WeekStartDate = requestModel.WeekStartDate
            };
        }
    }
}
