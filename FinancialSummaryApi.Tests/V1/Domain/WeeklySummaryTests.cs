using FinancialSummaryApi.V1.Domain;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Domain
{
    public class WeeklySummaryTests
    {
        [Fact]
        public void WeeklySummaryHasPropertiesSet()
        {
            var weeklySummary = new WeeklySummary()
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                FinancialMonth = 7,
                FinancialYear = 2021,
                PeriodNo = 1,
                WeekStartDate = new DateTime(2021, 7, 1),
                BalanceAmount = 50,
                PaidAmount = 37,
                ChargedAmount = 99,
                HousingBenefitAmount = 12
            };

            weeklySummary.Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            weeklySummary.TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            weeklySummary.PeriodNo.Should().Be(1);
            weeklySummary.FinancialYear.Should().Be(2021);
            weeklySummary.FinancialMonth.Should().Be(7);
            weeklySummary.WeekStartDate.Should().Be(new DateTime(2021, 7, 1));
            weeklySummary.ChargedAmount.Should().Be(99);
            weeklySummary.BalanceAmount.Should().Be(50);
            weeklySummary.PaidAmount.Should().Be(37);
            weeklySummary.HousingBenefitAmount.Should().Be(12);
        }
    }
}
