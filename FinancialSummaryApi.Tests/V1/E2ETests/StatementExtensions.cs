using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.E2ETests
{
    public static class StatementExtensions
    {
        public static void ShouldBeEqualTo(this StatementResponse response, Statement model)
        {
            response.Should().NotBeNull();
            model.Should().NotBeNull();

            response.TargetId.Should().Be(model.TargetId);
            response.TargetType.Should().Be(model.TargetType);
            response.StatementPeriodEndDate.Should().Be(model.StatementPeriodEndDate);
            response.RentAccountNumber.Should().Be(model.RentAccountNumber);
            response.Address.Should().Be(model.Address);
            response.StatementType.Should().Be(model.StatementType);
            response.ChargedAmount.Should().Be(model.ChargedAmount);
            response.PaidAmount.Should().Be(model.PaidAmount);
            response.HousingBenefitAmount.Should().Be(model.HousingBenefitAmount);
            response.StartBalance.Should().Be(model.StartBalance);
            response.FinishBalance.Should().Be(model.FinishBalance);
        }
    }
}
