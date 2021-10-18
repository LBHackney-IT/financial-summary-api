using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    public class StatementFactoryTest
    {
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = new StatementDbEntity()
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                SummaryType = SummaryType.Statement,
                StatementPeriodEndDate = new DateTime(2021, 8, 1),
                RentAccountNumber = "987654321",
                Address = "14 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 300,
                PaidAmount = 600,
                HousingBenefitAmount = 400,
                StartBalance = 1100,
                FinishBalance = 500
            }; 

            var domain = databaseEntity.ToDomain();

            domain.Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            domain.TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            domain.TargetType.Should().Be(TargetType.Block);
            domain.StatementPeriodEndDate.Should().Be(new DateTime(2021, 8, 1));
            domain.RentAccountNumber.Should().Be("987654321");
            domain.Address.Should().Be("14 Macron Court, E8 1ND");
            domain.StatementType.Should().Be(StatementType.Tenant);
            domain.ChargedAmount.Should().Be(300);
            domain.PaidAmount.Should().Be(600);
            domain.HousingBenefitAmount.Should().Be(400);
            domain.StartBalance.Should().Be(1100);
            domain.FinishBalance.Should().Be(500);
        }

        [Fact]
        public void CanMapDomainEntityToADatabaseObject()
        {
            var entity = new Statement()
            {
                Id = new Guid("4983a920-dcac-48e5-90f6-31195a2dcb69"),
                TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 8, 3),
                RentAccountNumber = "987654321",
                Address = "15 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 350,
                PaidAmount = 600,
                HousingBenefitAmount = 400,
                StartBalance = 1100,
                FinishBalance = 500
            };

            var databaseEntity = entity.ToDatabase();

            databaseEntity.Id.Should().Be(new Guid("4983a920-dcac-48e5-90f6-31195a2dcb69"));
            databaseEntity.TargetId.Should().Be(new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"));
            databaseEntity.TargetType.Should().Be(TargetType.Block);
            databaseEntity.SummaryType.Should().Be(SummaryType.Statement);
            databaseEntity.StatementPeriodEndDate.Should().Be(new DateTime(2021, 8, 3));
            databaseEntity.RentAccountNumber.Should().Be("987654321");
            databaseEntity.Address.Should().Be("15 Macron Court, E8 1ND");
            databaseEntity.StatementType.Should().Be(StatementType.Tenant);
            databaseEntity.ChargedAmount.Should().Be(350);
            databaseEntity.PaidAmount.Should().Be(600);
            databaseEntity.HousingBenefitAmount.Should().Be(400);
            databaseEntity.StartBalance.Should().Be(1100);
            databaseEntity.FinishBalance.Should().Be(500);
        }

        [Fact]
        public void CanMapAddRequestEntityToADomainObject()
        {
            var entity = new AddStatementRequest()
            {
                TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2020, 8, 3),
                RentAccountNumber = "987654321",
                Address = "16 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 350,
                PaidAmount = 600,
                HousingBenefitAmount = 800,
                StartBalance = 1100,
                FinishBalance = 500
            };

            var domain = entity.ToDomain();

            domain.Id.Should().Be(Guid.Empty);
            domain.TargetId.Should().Be(new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"));
            domain.TargetType.Should().Be(TargetType.Block);
            domain.StatementPeriodEndDate.Should().Be(new DateTime(2020, 8, 3));
            domain.RentAccountNumber.Should().Be("987654321");
            domain.Address.Should().Be("16 Macron Court, E8 1ND");
            domain.StatementType.Should().Be(StatementType.Tenant);
            domain.ChargedAmount.Should().Be(350);
            domain.PaidAmount.Should().Be(600);
            domain.HousingBenefitAmount.Should().Be(800);
            domain.StartBalance.Should().Be(1100);
            domain.FinishBalance.Should().Be(500);
        }
    }
}
