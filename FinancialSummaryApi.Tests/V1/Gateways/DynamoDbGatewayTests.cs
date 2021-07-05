using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
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

            var assetSummary = await _gateway.GetAssetSummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummary.Should().BeNull();
        }

        [Test]
        public async Task GetAssetSummaryByTargetIdReturnsAssetSummaryIfItExists()
        {
            var firstEntity = _fixture.Create<FinanceSummaryDbEntity>();
            firstEntity.SubmitDate = new DateTime(2021, 7, 2, 14, 30, 10);
            var secondEntity = _fixture.Create<FinanceSummaryDbEntity>();
            secondEntity.SubmitDate = new DateTime(2021, 7, 2, 15, 50, 20);

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>()
                {
                    firstEntity,
                    secondEntity
                });

            var assetSummaryDomain = await _gateway.GetAssetSummaryByIdAsync(secondEntity.TargetId, new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummaryDomain.ShouldBeEqualTo(secondEntity);
        }

        [Test]
        public async Task GetAllAssetSummariesByDateReturnsList()
        {
            var firstEntity = _fixture.Create<FinanceSummaryDbEntity>();
            firstEntity.SubmitDate = new DateTime(2021, 7, 2);
            var secondEntity = _fixture.Create<FinanceSummaryDbEntity>();
            secondEntity.SubmitDate = new DateTime(2021, 7, 2);

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>()
                {
                    firstEntity,
                    secondEntity
                });

            var assetSummaries = await _gateway.GetAllAssetSummaryAsync(new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummaries.Should().HaveCount(2);

            assetSummaries[0].ShouldBeEqualTo(firstEntity);
            assetSummaries[1].ShouldBeEqualTo(secondEntity);
        }

        [Test]
        public async Task AddAssetSummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<AssetSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Test]
        public async Task AddAssetSummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((AssetSummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Test]
        public async Task GetRentGroupSummaryByRentGroupNameReturnsNullIfEntityDoesntExist()
        {
            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>());

            var rentGroupSummary = await _gateway.GetRentGroupSummaryByNameAsync("LeaseHolders", new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            rentGroupSummary.Should().BeNull();
        }

        [Test]
        public async Task GetRentGroupSummaryByRentGroupNameReturnsRentGroupSummaryIfItExists()
        {
            var firstEntity = _fixture.Create<FinanceSummaryDbEntity>();
            firstEntity.SubmitDate = new DateTime(2021, 7, 2, 23, 10, 0);
            firstEntity.RentGroupSummaryData.RentGroupName = "LeaseHolders";
            var secondEntity = _fixture.Create<FinanceSummaryDbEntity>();
            secondEntity.SubmitDate = new DateTime(2021, 7, 2, 15, 20, 10);
            secondEntity.RentGroupSummaryData.RentGroupName = "LeaseHolders";

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>()
                {
                    firstEntity,
                    secondEntity
                });

            var rentGroupSummaryDomain = await _gateway.GetRentGroupSummaryByNameAsync("LeaseHolders", new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            rentGroupSummaryDomain.ShouldBeEqualTo(firstEntity);
        }

        [Test]
        public async Task GetAllRentGroupSummariesByDateReturnsList()
        {
            var firstEntity = _fixture.Create<FinanceSummaryDbEntity>();
            firstEntity.SubmitDate = new DateTime(2021, 7, 2);
            var secondEntity = _fixture.Create<FinanceSummaryDbEntity>();
            secondEntity.SubmitDate = new DateTime(2021, 7, 2);

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>()
                {
                    firstEntity,
                    secondEntity
                });

            var assetSummaries = await _gateway.GetAllAssetSummaryAsync(new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummaries.Should().HaveCount(2);

            assetSummaries[0].ShouldBeEqualTo(firstEntity);
            assetSummaries[1].ShouldBeEqualTo(secondEntity);
        }

        [Test]
        public async Task AddRentGroupSummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<RentGroupSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Test]
        public async Task AddRentGroupSummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((RentGroupSummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }
    }
}
