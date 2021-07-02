using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FinancialSummaryApi.Tests.V1.Helper;
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
        public async Task GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>());

            var response = await _gateway.GetAssetSummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), DateTime.UtcNow).ConfigureAwait(false);

            response.Should().BeNull();
        }

        [Test]
        public void GetEntityByIdReturnsTheEntityIfItExists()
        {
            var latestEntity = _fixture.Create<FinanceSummaryDbEntity>();

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<FinanceSummaryDbEntity>()
                {
                    latestEntity,
                    _fixture.Create<FinanceSummaryDbEntity>()
                });

            var response = _gateway.GetAssetSummaryByIdAsync(latestEntity.Id, DateTime.UtcNow);

            latestEntity.Id.Should().Be(response.Result.Id);
            // ToDo
        }
    }
}
