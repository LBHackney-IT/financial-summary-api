using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public static class AssertExtensions
    {
        public static void ShouldBeEqualTo(this AssetSummary assetSummary, AssetSummaryDbEntity dbEntity)
        {
            dbEntity.Should().NotBeNull();

            assetSummary.Id.Should().Be(dbEntity.Id);
            assetSummary.TargetId.Should().Be(dbEntity.TargetId);
            assetSummary.TargetType.Should().Be(dbEntity.TargetType);
            assetSummary.SubmitDate.Should().Be(dbEntity.SubmitDate);
            assetSummary.AssetName.Should().Be(dbEntity.TargetName);
            assetSummary.TotalDwellingRent.Should().Be(dbEntity.TotalDwellingRent);
            assetSummary.TotalNonDwellingRent.Should().Be(dbEntity.TotalNonDwellingRent);
            assetSummary.TotalRentalServiceCharge.Should().Be(dbEntity.TotalRentalServiceCharge);
            assetSummary.TotalServiceCharges.Should().Be(dbEntity.TotalServiceCharges);
            assetSummary.TotalIncome.Should().Be(dbEntity.TotalIncome);
            assetSummary.TotalExpenditure.Should().Be(dbEntity.TotalExpenditure);
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
    }
}
