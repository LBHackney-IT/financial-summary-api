using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.E2ETests
{
    public static class AssertExtensions
    {
        public static void ShouldBeEqualTo(this AssetSummaryResponse response, AssetSummary model)
        {
            response.Should().NotBeNull();
            model.Should().NotBeNull();

            response.TargetId.Should().Be(model.TargetId);
            response.TargetType.Should().Be(model.TargetType);
            response.TotalDwellingRent.Should().Be(model.TotalDwellingRent);
            response.TotalNonDwellingRent.Should().Be(model.TotalNonDwellingRent);
            response.TotalRentalServiceCharge.Should().Be(model.TotalRentalServiceCharge);
            response.TotalServiceCharges.Should().Be(model.TotalServiceCharges);
            response.TotalIncome.Should().Be(model.TotalIncome);
            response.TotalExpenditure.Should().Be(model.TotalExpenditure);
            response.SubmitDate.Should().Be(model.SubmitDate);
        }
    }
}
