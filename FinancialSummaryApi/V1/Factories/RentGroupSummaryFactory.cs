using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialSummaryApi.V1.Factories
{
    public static class RentGroupSummaryFactory
    {
        public static RentGroupSummary ToDomain(this RentGroupSummaryDbEntity databaseEntity)
        {
            return databaseEntity == null ? null : new RentGroupSummary
            {
                Id = databaseEntity.Id,
                SubmitDate = databaseEntity.SubmitDate,
                TotalArrears = databaseEntity.TotalArrears,
                ChargedYTD = databaseEntity.ChargedYTD,
                PaidYTD = databaseEntity.PaidYTD,
                TargetDescription = databaseEntity.TargetDescription,
                TotalBalance = databaseEntity.TotalBalance,
                TotalCharged = databaseEntity.TotalCharged,
                TotalPaid = databaseEntity.TotalPaid,
                RentGroupName = databaseEntity.TargetName,
            };
        }

        public static RentGroupSummaryDbEntity ToDatabase(this RentGroupSummary entity, Guid targetId)
        {
            return entity == null ? null : new RentGroupSummaryDbEntity
            {
                TargetId = targetId,
                Id = entity.Id,
                SubmitDate = entity.SubmitDate,
                TotalArrears = entity.TotalArrears,
                ChargedYTD = entity.ChargedYTD,
                PaidYTD = entity.PaidYTD,
                TargetDescription = entity.TargetDescription,
                TotalBalance = entity.TotalBalance,
                TotalCharged = entity.TotalCharged,
                TotalPaid = entity.TotalPaid,
                TargetName = entity.RentGroupName,
                SummaryType = SummaryType.RentGroupSummary,
            };
        }

        public static RentGroupSummary ToDomain(this AddRentGroupSummaryRequest model)
        {
            return model == null ? null : new RentGroupSummary
            {
                SubmitDate = model.SubmitDate,
                TotalArrears = model.TotalArrears,
                ChargedYTD = model.ChargedYTD,
                PaidYTD = model.PaidYTD,
                TargetDescription = model.TargetDescription,
                TotalBalance = model.TotalBalance,
                TotalCharged = model.TotalCharged,
                TotalPaid = model.TotalPaid,
                RentGroupName = model.RentGroupName
            };
        }
        public static List<RentGroupSummary> ToDomain(this IEnumerable<RentGroupSummaryDbEntity> databaseEntity)
        {
            return databaseEntity.Select(p => p.ToDomain())
                                .OrderByDescending(x => x.SubmitDate)
                                .ToList();
        }
    }
}
