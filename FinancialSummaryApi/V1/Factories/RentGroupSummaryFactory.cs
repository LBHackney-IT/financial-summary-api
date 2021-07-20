using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;

namespace FinancialSummaryApi.V1.Factories
{
    public static class RentGroupSummaryFactory
    {
        public static RentGroupSummary ToRentGroupDomain(this FinanceSummaryDbEntity databaseEntity)
        {
            if (databaseEntity == null)
            {
                return null;
            }

            if (databaseEntity.RentGroupSummaryData == null)
            {
                throw new Exception("Loaded data from the database cannot be parsed as a RentGroup data. Id: " + databaseEntity.Id);
            }
            return new RentGroupSummary
            {
                Id = databaseEntity.Id,
                TargetType = databaseEntity.TargetType,
                SubmitDate = databaseEntity.SubmitDate,
                ArrearsYTD = databaseEntity.RentGroupSummaryData.ArrearsYTD,
                ChargedYTD = databaseEntity.RentGroupSummaryData.ChargedYTD,
                PaidYTD = databaseEntity.RentGroupSummaryData.PaidYTD,
                TargetDescription = databaseEntity.RentGroupSummaryData.TargetDescription,
                TotalBalance = databaseEntity.RentGroupSummaryData.TotalBalance,
                TotalCharged = databaseEntity.RentGroupSummaryData.TotalCharged,
                TotalPaid = databaseEntity.RentGroupSummaryData.TotalPaid,
                RentGroupName = databaseEntity.RentGroupSummaryData.RentGroupName
            };
        }

        public static FinanceSummaryDbEntity ToDatabase(this RentGroupSummary entity)
        {
            return entity == null ? null : new FinanceSummaryDbEntity
            {
                Id = entity.Id,
                TargetType = entity.TargetType,
                SubmitDate = entity.SubmitDate,
                RentGroupSummaryData = new RentGroupSummaryDbEntity()
                {
                    ArrearsYTD = entity.ArrearsYTD,
                    ChargedYTD = entity.ChargedYTD,
                    PaidYTD = entity.PaidYTD,
                    TargetDescription = entity.TargetDescription,
                    TotalBalance = entity.TotalBalance,
                    TotalCharged = entity.TotalCharged,
                    TotalPaid = entity.TotalPaid,
                    RentGroupName = entity.RentGroupName
                }
            };
        }

        public static RentGroupSummary ToRentGroupDomain(this AddRentGroupSummaryRequest model)
        {
            return model == null ? null : new RentGroupSummary
            {
                TargetType = model.TargetType,
                SubmitDate = model.SubmitDate,
                ArrearsYTD = model.ArrearsYTD,
                ChargedYTD = model.ChargedYTD,
                PaidYTD = model.PaidYTD,
                TargetDescription = model.TargetDescription,
                TotalBalance = model.TotalBalance,
                TotalCharged = model.TotalCharged,
                TotalPaid = model.TotalPaid,
                RentGroupName = model.RentGroupName
            };
        }
    }
}
