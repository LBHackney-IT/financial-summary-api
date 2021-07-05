using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    [TestFixture]
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IDynamoDBContext> _dynamoDb;
        private Mock<DynamoDbContextWrapper> _wrapper;
        private DynamoDbGateway _gateway;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _wrapper = new Mock<DynamoDbContextWrapper>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _wrapper.Object);
        }

        [Test]
        public async Task GetAssetSummaryByTargetIdReturnsNullIfEntityDoesntExist()
        {
            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>());

            var response = await _gateway.GetAssetSummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), DateTime.UtcNow)
                .ConfigureAwait(false);

            response.Should().BeNull();
        }

        [Test]
        public async Task GetAssetSummaryByTargetIdReturnsAssetSummaryIfItExists()
        {
            var firstEntity = _fixture.Create<FinanceSummaryDbEntity>();
            firstEntity.SubmitDate = new DateTime(2021, 7, 1);
            var latestEntity = _fixture.Create<FinanceSummaryDbEntity>();
            latestEntity.SubmitDate = new DateTime(2021, 7, 2);

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>()
                {
                    firstEntity,
                    latestEntity
                });

            var assetSummaryDomain = await _gateway.GetAssetSummaryByIdAsync(latestEntity.TargetId, DateTime.UtcNow)
                .ConfigureAwait(false);

            latestEntity.Id.Should().Be(assetSummaryDomain.Id);
            latestEntity.TargetId.Should().Be(assetSummaryDomain.TargetId);
            latestEntity.TargetType.Should().Be(assetSummaryDomain.TargetType);
            latestEntity.AssetSummaryData.AssetName.Should().Be(assetSummaryDomain.AssetName);
            latestEntity.AssetSummaryData.TotalDwellingRent.Should().Be(assetSummaryDomain.TotalDwellingRent);
            latestEntity.AssetSummaryData.TotalNonDwellingRent.Should().Be(assetSummaryDomain.TotalNonDwellingRent);
            latestEntity.AssetSummaryData.TotalRentalServiceCharge.Should().Be(assetSummaryDomain.TotalRentalServiceCharge);
            latestEntity.AssetSummaryData.TotalServiceCharges.Should().Be(assetSummaryDomain.TotalServiceCharges);
            latestEntity.SubmitDate.Should().Be(assetSummaryDomain.SubmitDate);
        }
    }
}
