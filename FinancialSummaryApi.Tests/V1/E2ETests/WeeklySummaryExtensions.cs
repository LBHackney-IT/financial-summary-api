using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.E2ETests
{
    public static class WeeklySummaryExtensions
    {
        public static void ShouldBeEqualTo(this WeeklySummaryResponse response, WeeklySummary model)
        {
            response.Should().NotBeNull();
            model.Should().NotBeNull();

            response.TargetId.Should().Be(model.TargetId);
            response.PeriodNo.Should().Be(model.PeriodNo);
            response.FinancialMonth.Should().Be(model.FinancialMonth);
            response.FinancialYear.Should().Be(model.FinancialYear);
            response.ChargedAmount.Should().Be(model.ChargedAmount);
            response.PaidAmount.Should().Be(model.PaidAmount);
            response.BalanceAmount.Should().Be(model.BalanceAmount);
            response.WeekStartDate.Date.Should().BeSameDateAs(model.WeekStartDate.Date);
            response.HousingBenefitAmount.Should().Be(model.HousingBenefitAmount);
        }
    }
}
