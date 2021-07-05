using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.E2ETests
{
    public static class RentGroupExtensions
    {
        public static void ShouldBeEqualTo(this RentGroupSummaryResponse response, RentGroupSummary model)
        {
            response.Should().NotBeNull();
            model.Should().NotBeNull();

            response.TargetType.Should().Be(model.TargetType);
            response.SubmitDate.Should().Be(model.SubmitDate);
            response.ArrearsYTD.Should().Be(model.ArrearsYTD);
            response.ChargedYTD.Should().Be(model.ChargedYTD);
            response.PaidYTD.Should().Be(model.PaidYTD);
            response.TargetDescription.Should().Be(model.TargetDescription);
            response.TotalBalance.Should().Be(model.TotalBalance);
            response.TotalCharged.Should().Be(model.TotalCharged);
            response.TotalPaid.Should().Be(model.TotalPaid);
            response.RentGroupName.Should().Be(model.RentGroupName);
        }
    }
}
