using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public static class AssertExtensions
    {
        public static void ShouldBeEqualTo(this AssetSummary assetSummary, FinanceSummaryDbEntity dbEntity)
        {
            dbEntity.Should().NotBeNull();

            assetSummary.Id.Should().Be(dbEntity.Id);
            assetSummary.TargetId.Should().Be(dbEntity.TargetId);
            assetSummary.TargetType.Should().Be(dbEntity.TargetType);
            assetSummary.SubmitDate.Should().Be(dbEntity.SubmitDate);
            assetSummary.AssetName.Should().Be(dbEntity.AssetSummaryData.AssetName);
            assetSummary.TotalDwellingRent.Should().Be(dbEntity.AssetSummaryData.TotalDwellingRent);
            assetSummary.TotalNonDwellingRent.Should().Be(dbEntity.AssetSummaryData.TotalNonDwellingRent);
            assetSummary.TotalRentalServiceCharge.Should().Be(dbEntity.AssetSummaryData.TotalRentalServiceCharge);
            assetSummary.TotalServiceCharges.Should().Be(dbEntity.AssetSummaryData.TotalServiceCharges);
            assetSummary.TotalIncome.Should().Be(dbEntity.AssetSummaryData.TotalIncome);
            assetSummary.TotalExpenditure.Should().Be(dbEntity.AssetSummaryData.TotalExpenditure);
        }

        public static void ShouldBeEqualTo(this AssetSummary thisAssetSummary, AssetSummary assetSummary)
        {
            assetSummary.Should().NotBeNull();

            thisAssetSummary.Id.Should().Be(assetSummary.Id);
            thisAssetSummary.TargetId.Should().Be(assetSummary.TargetId);
            thisAssetSummary.TargetType.Should().Be(assetSummary.TargetType);
            thisAssetSummary.SubmitDate.Should().Be(assetSummary.SubmitDate);
            thisAssetSummary.AssetName.Should().Be(assetSummary.AssetName);
            thisAssetSummary.TotalDwellingRent.Should().Be(assetSummary.TotalDwellingRent);
            thisAssetSummary.TotalNonDwellingRent.Should().Be(assetSummary.TotalNonDwellingRent);
            thisAssetSummary.TotalRentalServiceCharge.Should().Be(assetSummary.TotalRentalServiceCharge);
            thisAssetSummary.TotalServiceCharges.Should().Be(assetSummary.TotalServiceCharges);
            thisAssetSummary.TotalIncome.Should().Be(assetSummary.TotalIncome);
            thisAssetSummary.TotalExpenditure.Should().Be(assetSummary.TotalExpenditure);
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
                    AssetName = entity.AssetName,
                    TotalDwellingRent = entity.TotalDwellingRent,
                    TotalNonDwellingRent = entity.TotalNonDwellingRent,
                    TotalRentalServiceCharge = entity.TotalRentalServiceCharge,
                    TotalServiceCharges = entity.TotalServiceCharges,
                    TotalIncome = entity.TotalIncome,
                    TotalExpenditure = entity.TotalExpenditure
                }
            };
        }
    }
}
