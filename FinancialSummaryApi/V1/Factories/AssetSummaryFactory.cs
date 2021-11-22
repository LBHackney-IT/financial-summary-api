using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;

namespace FinancialSummaryApi.V1.Factories
{
    public static class AssetSummaryFactory
    {
        public static AssetSummary ToDomain(this AssetSummaryDbEntity databaseEntity)
        {
            return databaseEntity == null ? null : new AssetSummary
            {
                Id = databaseEntity.Id,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                AssetName = databaseEntity.TargetName,
                SubmitDate = databaseEntity.SubmitDate,
                TotalDwellingRent = databaseEntity.TotalDwellingRent,
                TotalNonDwellingRent = databaseEntity.TotalNonDwellingRent,
                TotalRentalServiceCharge = databaseEntity.TotalRentalServiceCharge,
                TotalServiceCharges = databaseEntity.TotalServiceCharges,
                TotalIncome = databaseEntity.TotalIncome,
                TotalExpenditure = databaseEntity.TotalExpenditure,
            };
        }

        public static AssetSummaryDbEntity ToDatabase(this AssetSummary entity, string partitionKey)
        {
            return entity == null ? null : new AssetSummaryDbEntity
            {
                Pk = partitionKey,
                Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                SubmitDate = entity.SubmitDate,
                TargetName = entity.AssetName,
                TotalDwellingRent = entity.TotalDwellingRent,
                TotalNonDwellingRent = entity.TotalNonDwellingRent,
                TotalRentalServiceCharge = entity.TotalRentalServiceCharge,
                TotalServiceCharges = entity.TotalServiceCharges,
                TotalIncome = entity.TotalIncome,
                TotalExpenditure = entity.TotalExpenditure,
                SummaryType = SummaryType.AssetSummary
            };
        }

        public static AssetSummary ToDomain(this AddAssetSummaryRequest requestModel)
        {
            return requestModel == null ? null : new AssetSummary
            {
                TargetId = requestModel.TargetId,
                TargetType = requestModel.TargetType,
                AssetName = requestModel.AssetName,
                SubmitDate = requestModel.SubmitDate,
                TotalDwellingRent = requestModel.TotalDwellingRent,
                TotalNonDwellingRent = requestModel.TotalNonDwellingRent,
                TotalRentalServiceCharge = requestModel.TotalRentalServiceCharge,
                TotalServiceCharges = requestModel.TotalServiceCharges,
                TotalIncome = requestModel.TotalIncome,
                TotalExpenditure = requestModel.TotalExpenditure,
            };
        }
    }
}
