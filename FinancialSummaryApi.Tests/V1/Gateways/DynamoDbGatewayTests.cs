using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly Mock<DynamoDbContextWrapper> _wrapper;
        private readonly DynamoDbGateway _gateway;

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _wrapper = new Mock<DynamoDbContextWrapper>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _wrapper.Object);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
        public async Task AddAssetSummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<AssetSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddAssetSummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((AssetSummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
        public async Task AddRentGroupSummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<RentGroupSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddRentGroupSummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((RentGroupSummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<FinanceSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task GetWeeklySummaryByTargetIdReturnsNullIfEntityDoesntExist()
        {
            _wrapper.Setup(_ => _.ScanSummaryAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<WeeklySummaryDbEntity>());

            var weeklySummary = await _gateway.GetWeeklySummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"))
                .ConfigureAwait(false);

            weeklySummary.Should().BeNull();
        }

        [Fact]
        public async Task GetWeeklySummaryByTargetIdReturnsWeeklySummaryIfItExists()
        {
            var firstEntity = _fixture.Create<WeeklySummaryDbEntity>();
            firstEntity.WeekStartDate = new DateTime(2021, 7, 2, 14, 30, 10);
            
            _wrapper.Setup(_ => _.LoadSummaryAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<Guid>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(firstEntity);

            var weeklySummaryDomain = await _gateway.GetWeeklySummaryByIdAsync(firstEntity.TargetId)
                .ConfigureAwait(false);

            weeklySummaryDomain.Should().BeEquivalentTo(firstEntity);
        }

        [Fact]
        public async Task GetAllWeeklySummariesByDateReturnsList()
        {
            var firstEntity = _fixture.Create<WeeklySummaryDbEntity>();
            firstEntity.WeekStartDate = new DateTime(2021, 7, 2);
            var secondEntity = _fixture.Create<WeeklySummaryDbEntity>();
            secondEntity.WeekStartDate = new DateTime(2021, 7, 2);
            secondEntity.TargetId = firstEntity.TargetId;

            _wrapper.Setup(_ => _.ScanSummaryAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<WeeklySummaryDbEntity>()
                {
                    firstEntity,
                    secondEntity
                });

            var weeklySummaries = await _gateway.GetAllWeeklySummaryAsync(firstEntity.TargetId, new DateTime(2021-6-2), new DateTime(2021-7-2))
                .ConfigureAwait(false);

            weeklySummaries.Should().HaveCount(2);

            weeklySummaries[0].Should().BeEquivalentTo(firstEntity);
            weeklySummaries[1].Should().BeEquivalentTo(secondEntity);
        }


        [Fact]
        public async Task AddWeeklySummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<WeeklySummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddWeeklySummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((WeeklySummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), default), Times.Once);
        }
    }
}
