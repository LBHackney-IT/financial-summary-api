using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public static class RentGroupExtensions
    {
        public static void ShouldBeEqualTo(this RentGroupSummary rentGroupSummary, FinanceSummaryDbEntity dbEntity)
        {
            dbEntity.Should().NotBeNull();

            rentGroupSummary.Id.Should().Be(dbEntity.Id);
            rentGroupSummary.TargetType.Should().Be(dbEntity.TargetType);
            rentGroupSummary.SubmitDate.Should().Be(dbEntity.SubmitDate);
            rentGroupSummary.RentGroupName.Should().Be(dbEntity.RentGroupSummaryData.RentGroupName);
            rentGroupSummary.TargetDescription.Should().Be(dbEntity.RentGroupSummaryData.TargetDescription);
            rentGroupSummary.TotalCharged.Should().Be(dbEntity.RentGroupSummaryData.TotalCharged);
            rentGroupSummary.TotalBalance.Should().Be(dbEntity.RentGroupSummaryData.TotalBalance);
            rentGroupSummary.TotalPaid.Should().Be(dbEntity.RentGroupSummaryData.TotalPaid);
            rentGroupSummary.ChargedYTD.Should().Be(dbEntity.RentGroupSummaryData.ChargedYTD);
            rentGroupSummary.ArrearsYTD.Should().Be(dbEntity.RentGroupSummaryData.ArrearsYTD);
            rentGroupSummary.PaidYTD.Should().Be(dbEntity.RentGroupSummaryData.PaidYTD);
        }
    }
}
