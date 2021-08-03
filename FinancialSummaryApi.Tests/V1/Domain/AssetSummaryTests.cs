using System;
using FinancialSummaryApi.V1.Domain;
using FluentAssertions;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Domain
{
    public class AssetSummaryTests
    {
        [Fact]
        public void AssetSummaryHasPropertiesSet()
        {
            var assetSummary = new AssetSummary()
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Estate,
                SubmitDate = new DateTime(2021, 7, 1),
                TotalDwellingRent = 178,
                TotalNonDwellingRent = 67,
                TotalRentalServiceCharge = 120,
                TotalServiceCharges = 213,
                TotalIncome = 242,
                TotalExpenditure = 111,
                AssetName = "Estate 1"
            };

            assetSummary.Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            assetSummary.TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            assetSummary.TargetType.Should().Be(TargetType.Estate);
            assetSummary.SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            assetSummary.TotalDwellingRent.Should().Be(178);
            assetSummary.TotalNonDwellingRent.Should().Be(67);
            assetSummary.TotalRentalServiceCharge.Should().Be(120);
            assetSummary.TotalServiceCharges.Should().Be(213);
            assetSummary.TotalIncome.Should().Be(242);
            assetSummary.TotalExpenditure.Should().Be(111);
            assetSummary.AssetName.Should().Be("Estate 1");
        }
    }
}
