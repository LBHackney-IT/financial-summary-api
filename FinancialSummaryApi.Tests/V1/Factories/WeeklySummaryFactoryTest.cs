using FinancialSummaryApi.V1.Controllers;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    public class WeeklySummaryFactoryTest
    {
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = new WeeklySummaryDbEntity
            {
                Id = new Guid("4983a920-dcac-48e5-90f6-31195a2dcb69"),
                TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                FinancialMonth = 7,
                FinancialYear = 2021,
                WeekStartDate = new DateTime(2021, 7, 2),
                PeriodNo = 1,
                BalanceAmount = 20,
                ChargedAmount = 150,
                PaidAmount = 120,
                HousingBenefitAmount = 10
            };

            var entity = databaseEntity.ToDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.TargetId.Should().Be(entity.TargetId);
            databaseEntity.PeriodNo.Should().Be(entity.PeriodNo);
            databaseEntity.WeekStartDate.Should().Be(entity.WeekStartDate);
            databaseEntity.FinancialMonth.Should().Be(entity.FinancialMonth);
            databaseEntity.FinancialYear.Should().Be(entity.FinancialYear);
            databaseEntity.ChargedAmount.Should().Be(entity.ChargedAmount);
            databaseEntity.PaidAmount.Should().Be(entity.PaidAmount);
            databaseEntity.BalanceAmount.Should().Be(entity.BalanceAmount);
            databaseEntity.HousingBenefitAmount.Should().Be(entity.HousingBenefitAmount);
        }

        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = new WeeklySummary
            {
                Id = new Guid("4a5b61f6-dd03-4803-9dc2-80fa3b18a7ab"),
                TargetId = new Guid("3ffe26ad-be50-4789-b509-9379972f07bb"),
                FinancialMonth = 7,
                FinancialYear = 2021,
                WeekStartDate = new DateTime(2021, 7, 2),
                PeriodNo = 1,
                BalanceAmount = 20,
                ChargedAmount = 150,
                PaidAmount = 120,
                HousingBenefitAmount = 10
            };

            var databaseEntity = entity.ToDatabase(Constants.PartitionKey);

            entity.Id.Should().Be(databaseEntity.Id);
            entity.TargetId.Should().Be(databaseEntity.TargetId);
            entity.PeriodNo.Should().Be(databaseEntity.PeriodNo);
            entity.WeekStartDate.Should().Be(databaseEntity.WeekStartDate);
            entity.FinancialMonth.Should().Be(databaseEntity.FinancialMonth);
            entity.FinancialYear.Should().Be(databaseEntity.FinancialYear);
            entity.ChargedAmount.Should().Be(databaseEntity.ChargedAmount);
            entity.PaidAmount.Should().Be(databaseEntity.PaidAmount);
            entity.BalanceAmount.Should().Be(databaseEntity.BalanceAmount);
            entity.HousingBenefitAmount.Should().Be(databaseEntity.HousingBenefitAmount);
        }
    }
}
