using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    [TestFixture]
    public class RentGroupSummaryFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Create<FinanceSummaryDbEntity>();
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

        [Test]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = _fixture.Create<RentGroupSummary>();
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
