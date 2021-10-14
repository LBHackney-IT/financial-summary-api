using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;

namespace FinancialSummaryApi.V1.Factories
{
    public static class StatementFactory
    {
        public static Statement ToDomain(this StatementDbEntity databaseEntity)
        {
            return databaseEntity == null ? null : new Statement
            {
                Id = databaseEntity.Id,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                StatementPeriodEndDate = databaseEntity.StatementPeriodEndDate,
                RentAccountNumber = databaseEntity.RentAccountNumber,
                Address = databaseEntity.Address,
                StatementType = databaseEntity.StatementType,
                ChargedAmount = databaseEntity.ChargedAmount,
                PaidAmount = databaseEntity.PaidAmount,
                HousingBenefitAmount = databaseEntity.HousingBenefitAmount,
                StartBalance = databaseEntity.StartBalance,
                EndBalance = databaseEntity.EndBalance
            };
        }

        public static StatementDbEntity ToDatabase(this Statement entity)
        {
            return entity == null ? null : new StatementDbEntity
            {
                Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                SummaryType = SummaryType.Statement,
                StatementPeriodEndDate = entity.StatementPeriodEndDate,
                RentAccountNumber = entity.RentAccountNumber,
                Address = entity.Address,
                StatementType = entity.StatementType,
                ChargedAmount = entity.ChargedAmount,
                PaidAmount = entity.PaidAmount,
                HousingBenefitAmount = entity.HousingBenefitAmount,
                StartBalance = entity.StartBalance,
                EndBalance = entity.EndBalance
            };
        }

        public static Statement ToDomain(this AddStatementRequest requestModel)
        {
            return requestModel == null ? null : new Statement
            {
                TargetId = requestModel.TargetId,
                TargetType = requestModel.TargetType,
                StatementPeriodEndDate = requestModel.StatementPeriodEndDate,
                RentAccountNumber = requestModel.RentAccountNumber,
                Address = requestModel.Address,
                StatementType = requestModel.StatementType,
                ChargedAmount = requestModel.ChargedAmount,
                PaidAmount = requestModel.PaidAmount,
                HousingBenefitAmount = requestModel.HousingBenefitAmount,
                StartBalance = requestModel.StartBalance,
                EndBalance = requestModel.EndBalance
            };
        }
    }
}
