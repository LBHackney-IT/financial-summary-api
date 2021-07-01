using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    [TestFixture]
    public class EntityFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDomain()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Create<FinanceSummaryDbEntity>();
            var entity = databaseEntity.ToAssetDomain();
            // ToDO
            databaseEntity.Id.Should().Be(entity.Id);
        }

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDatabase()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Test]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = _fixture.Create<AssetSummary>();
            var databaseEntity = entity.ToDatabase();
            // ToDo
            entity.Id.Should().Be(databaseEntity.Id);
        }
    }
}
