using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FinancialSummaryApi.V1.Factories
{
    public static class StatementFactory
    {

        public static Statement ToDomain(this StatementDbEntity statementDbEntity)
        {
            return statementDbEntity == null ? null : new Statement
            {
                Id = statementDbEntity.Id,
                TargetId = statementDbEntity.TargetId,
                Address = statementDbEntity.Address,
                ChargedAmount = statementDbEntity.ChargedAmount,
                FinishBalance = statementDbEntity.FinishBalance,
                HousingBenefitAmount = statementDbEntity.HousingBenefitAmount,
                PaidAmount = statementDbEntity.PaidAmount,
                RentAccountNumber = statementDbEntity.RentAccountNumber,
                StartBalance = statementDbEntity.StartBalance,
                StatementPeriodStartDate = statementDbEntity.StatementPeriodStartDate,
                StatementPeriodEndDate = statementDbEntity.StatementPeriodEndDate,
                StatementType = statementDbEntity.StatementType,
                TargetType = statementDbEntity.TargetType,
            };
        }
        public static List<Statement> ToDomain(this IEnumerable<StatementDbEntity> databaseEntity)
        {
            return databaseEntity.Select(p => p.ToDomain())
                                 .OrderBy(x => x.StatementPeriodEndDate)
                                 .ToList();
        }  
    }
}
