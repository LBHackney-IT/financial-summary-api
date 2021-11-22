using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;

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
                ArrearsYTD = databaseEntity.ArrearsYTD,
                ChargedYTD = databaseEntity.ChargedYTD,
                PaidYTD = databaseEntity.PaidYTD,
                TargetDescription = databaseEntity.TargetDescription,
                TotalBalance = databaseEntity.TotalBalance,
                TotalCharged = databaseEntity.TotalCharged,
                TotalPaid = databaseEntity.TotalPaid,
                RentGroupName = databaseEntity.TargetName,
            };
        }

        public static RentGroupSummaryDbEntity ToDatabase(this RentGroupSummary entity, string pk)
        {
            return entity == null ? null : new RentGroupSummaryDbEntity
            {
                Pk = pk,
                Id = entity.Id,
                SubmitDate = entity.SubmitDate,
                ArrearsYTD = entity.ArrearsYTD,
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
