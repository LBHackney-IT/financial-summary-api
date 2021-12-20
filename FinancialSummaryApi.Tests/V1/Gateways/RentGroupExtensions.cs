using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public static class RentGroupExtensions
    {
        public static void ShouldBeEqualTo(this RentGroupSummary rentGroupSummary, RentGroupSummaryDbEntity dbEntity)
        {
            dbEntity.Should().NotBeNull();

            rentGroupSummary.Id.Should().Be(dbEntity.Id);
            rentGroupSummary.SubmitDate.Should().Be(dbEntity.SubmitDate);
            rentGroupSummary.RentGroupName.Should().Be(dbEntity.TargetName);
            rentGroupSummary.TargetDescription.Should().Be(dbEntity.TargetDescription);
            rentGroupSummary.TotalCharged.Should().Be(dbEntity.TotalCharged);
            rentGroupSummary.TotalBalance.Should().Be(dbEntity.TotalBalance);
            rentGroupSummary.TotalPaid.Should().Be(dbEntity.TotalPaid);
            rentGroupSummary.ChargedYTD.Should().Be(dbEntity.ChargedYTD);
            rentGroupSummary.TotalArrears.Should().Be(dbEntity.TotalArrears);
            rentGroupSummary.PaidYTD.Should().Be(dbEntity.PaidYTD);
        }
    }
}
