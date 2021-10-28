using FinancialSummaryApi.V1.Domain;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Domain
{
    public class StatementTests
    {
        [Fact]
        public void StatementHasPropertiesSet()
        {
            var statement = new Statement()
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            statement.Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            statement.TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            statement.TargetType.Should().Be(TargetType.Block);
            statement.StatementPeriodEndDate.Should().Be(new DateTime(2021, 7, 1));
            statement.RentAccountNumber.Should().Be("123456789");
            statement.Address.Should().Be("12 Macron Court, E8 1ND");
            statement.StatementType.Should().Be(StatementType.Tenant);
            statement.ChargedAmount.Should().Be(200);
            statement.PaidAmount.Should().Be(500);
            statement.HousingBenefitAmount.Should().Be(300);
            statement.StartBalance.Should().Be(1000);
            statement.FinishBalance.Should().Be(400);
        }
    }
}
