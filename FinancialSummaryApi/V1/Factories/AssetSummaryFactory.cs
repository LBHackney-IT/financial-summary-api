using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;

namespace FinancialSummaryApi.V1.Factories
{
    public static class AssetSummaryFactory
    {
        public static AssetSummary ToAssetDomain(this FinanceSummaryDbEntity databaseEntity)
        {
            if(databaseEntity == null)
            {
                return null;
            }
            if (databaseEntity.AssetSummaryData == null)
            {
                throw new Exception("Loaded data from the database cannot be parsed as a Asset data. Id: " + databaseEntity.Id);
            }
            return new AssetSummary
            {
                Id = databaseEntity.Id,
                TargetId =  databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                SubmitDate = databaseEntity.SubmitDate,
                TotalDwellingRent = databaseEntity.AssetSummaryData.TotalDwellingRent,
                TotalNonDwellingRent = databaseEntity.AssetSummaryData.TotalNonDwellingRent,
                TotalRentalServiceCharge = databaseEntity.AssetSummaryData.TotalRentalServiceCharge,
                TotalServiceCharges = databaseEntity.AssetSummaryData.TotalServiceCharges
            };
        }

        public static FinanceSummaryDbEntity ToDatabase(this AssetSummary entity)
        {
            return entity == null ? null : new FinanceSummaryDbEntity
            {
                Id = entity.Id,
                TargetId = entity.TargetId,
                TargetType = entity.TargetType,
                SubmitDate = entity.SubmitDate,
                AssetSummaryData = new AssetSummaryDbEntity()
                {
                    TotalDwellingRent = entity.TotalDwellingRent,
                    TotalNonDwellingRent = entity.TotalNonDwellingRent,
                    TotalRentalServiceCharge = entity.TotalRentalServiceCharge,
                    TotalServiceCharges = entity.TotalServiceCharges
                }
            };
        }

        public static AssetSummary ToDomain(this AddAssetSummaryRequest requestModel)
        {
            return requestModel == null ? null : new AssetSummary
            {
                TargetId = requestModel.TargetId,
                TargetType = requestModel.TargetType,
                SubmitDate = requestModel.SubmitDate,
                TotalDwellingRent = requestModel.TotalDwellingRent,
                TotalNonDwellingRent = requestModel.TotalNonDwellingRent,
                TotalRentalServiceCharge = requestModel.TotalRentalServiceCharge,
                TotalServiceCharges = requestModel.TotalServiceCharges
            };
        }
    }
}
