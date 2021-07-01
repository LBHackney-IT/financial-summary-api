using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    [TestFixture]
    public class AssetSummaryFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            // ToDO: Use Real Value
            var databaseEntity = _fixture.Create<FinanceSummaryDbEntity>();
            var entity = databaseEntity.ToAssetDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.TargetId.Should().Be(entity.TargetId);
            databaseEntity.TargetType.Should().Be(entity.TargetType);
            databaseEntity.SubmitDate.Should().Be(entity.SubmitDate);
            databaseEntity.AssetSummaryData.TotalDwellingRent.Should().Be(entity.TotalDwellingRent);
            databaseEntity.AssetSummaryData.TotalNonDwellingRent.Should().Be(entity.TotalNonDwellingRent);
            databaseEntity.AssetSummaryData.TotalRentalServiceCharge.Should().Be(entity.TotalRentalServiceCharge);
            databaseEntity.AssetSummaryData.TotalServiceCharges.Should().Be(entity.TotalServiceCharges);
        }

        [Test]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = _fixture.Create<AssetSummary>();
            var databaseEntity = entity.ToDatabase();

            entity.Id.Should().Be(databaseEntity.Id);
            entity.TargetId.Should().Be(databaseEntity.TargetId);
            entity.TargetType.Should().Be(databaseEntity.TargetType);
            entity.SubmitDate.Should().Be(databaseEntity.SubmitDate);
            entity.TotalDwellingRent.Should().Be(databaseEntity.AssetSummaryData.TotalDwellingRent);
            entity.TotalNonDwellingRent.Should().Be(databaseEntity.AssetSummaryData.TotalNonDwellingRent);
            entity.TotalRentalServiceCharge.Should().Be(databaseEntity.AssetSummaryData.TotalRentalServiceCharge);
            entity.TotalServiceCharges.Should().Be(databaseEntity.AssetSummaryData.TotalServiceCharges);
        }
    }
}
