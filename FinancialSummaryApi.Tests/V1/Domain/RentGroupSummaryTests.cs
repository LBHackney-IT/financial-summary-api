using FinancialSummaryApi.V1.Domain;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Domain
{
    public class RentGroupSummaryTests
    {
        [Fact]
        public void RentGroupSummaryHasPropertiesSet()
        {
            var rentGroupSummary = new RentGroupSummary()
            {
                Id = new Guid("98d1a25e-4a88-4117-b789-c8b7a1e8e758"),
                TargetType = TargetType.RentGroup,
                SubmitDate = new DateTime(2021, 7, 1),
                ArrearsYTD = 150,
                ChargedYTD = 113,
                PaidYTD = 263,
                TargetDescription = "Description",
                TotalBalance = 100,
                TotalCharged = 263,
                TotalPaid = 363,
                RentGroupName = "RentGroupName"
            };

            rentGroupSummary.Id.Should().Be(new Guid("98d1a25e-4a88-4117-b789-c8b7a1e8e758"));
            rentGroupSummary.TargetType.Should().Be(TargetType.RentGroup);
            rentGroupSummary.SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            rentGroupSummary.ArrearsYTD.Should().Be(150);
            rentGroupSummary.ChargedYTD.Should().Be(113);
            rentGroupSummary.PaidYTD.Should().Be(263);
            rentGroupSummary.TargetDescription.Should().Be("Description");
            rentGroupSummary.TotalBalance.Should().Be(100);
            rentGroupSummary.TotalCharged.Should().Be(263);
            rentGroupSummary.TotalPaid.Should().Be(363);
            rentGroupSummary.RentGroupName.Should().Be("RentGroupName");
        }
    }
}
