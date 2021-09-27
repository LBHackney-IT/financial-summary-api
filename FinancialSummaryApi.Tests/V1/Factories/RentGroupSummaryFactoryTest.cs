using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    public class RentGroupSummaryFactoryTest
    {
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = new RentGroupSummaryDbEntity
            {
                Id = new Guid("98eb124b-b90e-452f-8916-94cd8e40b582"),
                SubmitDate = new DateTime(2021, 7, 2),
                TargetName = "LeaseHolders",
                TargetDescription = "desc",
                ArrearsYTD = 100,
                ChargedYTD = 120,
                PaidYTD = 150,
                TotalBalance = -70,
                TotalCharged = 170,
                TotalPaid = 150,
                SummaryType = SummaryType.RentGroupSummary
            };

            var entity = databaseEntity.ToDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.SummaryType.Should().Be(SummaryType.RentGroupSummary);
            databaseEntity.SubmitDate.Should().Be(entity.SubmitDate);
            databaseEntity.ArrearsYTD.Should().Be(entity.ArrearsYTD);
            databaseEntity.ChargedYTD.Should().Be(entity.ChargedYTD);
            databaseEntity.PaidYTD.Should().Be(entity.PaidYTD);
            databaseEntity.TargetDescription.Should().Be(entity.TargetDescription);
            databaseEntity.TotalBalance.Should().Be(entity.TotalBalance);
            databaseEntity.TotalCharged.Should().Be(entity.TotalCharged);
            databaseEntity.TotalPaid.Should().Be(entity.TotalPaid);
            databaseEntity.TargetName.Should().Be(entity.RentGroupName);
        }

        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = new RentGroupSummary
            {
                Id = new Guid("d597c0fc-9e32-4f65-894c-c8922bcfed64"),
                SubmitDate = new DateTime(2021, 7, 2),
                TargetDescription = "desc",
                RentGroupName = "LeaseHolders",
                ArrearsYTD = 150,
                ChargedYTD = 120,
                PaidYTD = 270,
                TotalBalance = 0,
                TotalCharged = 270,
                TotalPaid = 270
            };

            var databaseEntity = entity.ToDatabase();

            entity.Id.Should().Be(databaseEntity.Id);
            entity.SubmitDate.Should().Be(databaseEntity.SubmitDate);
            entity.ArrearsYTD.Should().Be(databaseEntity.ArrearsYTD);
            entity.ChargedYTD.Should().Be(databaseEntity.ChargedYTD);
            entity.PaidYTD.Should().Be(databaseEntity.PaidYTD);
            entity.TargetDescription.Should().Be(databaseEntity.TargetDescription);
            entity.TotalBalance.Should().Be(databaseEntity.TotalBalance);
            entity.TotalCharged.Should().Be(databaseEntity.TotalCharged);
            entity.TotalPaid.Should().Be(databaseEntity.TotalPaid);
            entity.RentGroupName.Should().Be(databaseEntity.TargetName);
        }
    }
}
