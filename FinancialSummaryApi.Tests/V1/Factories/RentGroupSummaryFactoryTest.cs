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
            var databaseEntity = new FinanceSummaryDbEntity
            {
                Id = new Guid("98eb124b-b90e-452f-8916-94cd8e40b582"),
                TargetType = TargetType.RentGroup,
                SubmitDate = new DateTime(2021, 7, 2),
                RentGroupSummaryData = new RentGroupSummaryDbEntity
                {
                    RentGroupName = "LeaseHolders",
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    ChargedYTD = 120,
                    PaidYTD = 150,
                    TotalBalance = -70,
                    TotalCharged = 170,
                    TotalPaid = 150
                }
            };

            var entity = databaseEntity.ToRentGroupDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.TargetType.Should().Be(entity.TargetType);
            databaseEntity.SubmitDate.Should().Be(entity.SubmitDate);
            databaseEntity.RentGroupSummaryData.ArrearsYTD.Should().Be(entity.ArrearsYTD);
            databaseEntity.RentGroupSummaryData.ChargedYTD.Should().Be(entity.ChargedYTD);
            databaseEntity.RentGroupSummaryData.PaidYTD.Should().Be(entity.PaidYTD);
            databaseEntity.RentGroupSummaryData.TargetDescription.Should().Be(entity.TargetDescription);
            databaseEntity.RentGroupSummaryData.TotalBalance.Should().Be(entity.TotalBalance);
            databaseEntity.RentGroupSummaryData.TotalCharged.Should().Be(entity.TotalCharged);
            databaseEntity.RentGroupSummaryData.TotalPaid.Should().Be(entity.TotalPaid);
            databaseEntity.RentGroupSummaryData.RentGroupName.Should().Be(entity.RentGroupName);
        }

        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = new RentGroupSummary
            {
                Id = new Guid("d597c0fc-9e32-4f65-894c-c8922bcfed64"),
                TargetType = TargetType.RentGroup,
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
            entity.TargetType.Should().Be(databaseEntity.TargetType);
            entity.SubmitDate.Should().Be(databaseEntity.SubmitDate);
            entity.ArrearsYTD.Should().Be(databaseEntity.RentGroupSummaryData.ArrearsYTD);
            entity.ChargedYTD.Should().Be(databaseEntity.RentGroupSummaryData.ChargedYTD);
            entity.PaidYTD.Should().Be(databaseEntity.RentGroupSummaryData.PaidYTD);
            entity.TargetDescription.Should().Be(databaseEntity.RentGroupSummaryData.TargetDescription);
            entity.TotalBalance.Should().Be(databaseEntity.RentGroupSummaryData.TotalBalance);
            entity.TotalCharged.Should().Be(databaseEntity.RentGroupSummaryData.TotalCharged);
            entity.TotalPaid.Should().Be(databaseEntity.RentGroupSummaryData.TotalPaid);
            entity.RentGroupName.Should().Be(databaseEntity.RentGroupSummaryData.RentGroupName);
        }
    }
}
