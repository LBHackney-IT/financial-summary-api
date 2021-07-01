using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using FinancialSummaryApi.Tests.V1.Helper;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    //TODO: Remove this file if DynamoDb gateway not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
    [TestFixture]
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IDynamoDBContext> _dynamoDb;
        private DynamoDbGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _classUnderTest = new DynamoDbGateway(_dynamoDb.Object);
        }

        [Test]
        public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            // ToDO
            var response = _classUnderTest.GetAssetSummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), DateTime.UtcNow.AddDays(-2));

            response.Should().BeNull();
        }

        [Test]
        public void GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<FinanceSummaryDbEntity>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(x => x.LoadAsync<FinanceSummaryDbEntity>(entity.Id, default))
                     .ReturnsAsync(dbEntity);
            // ToDO
            var response = _classUnderTest.GetAssetSummaryByIdAsync(entity.Id, DateTime.UtcNow);

            _dynamoDb.Verify(x => x.LoadAsync<FinanceSummaryDbEntity>(entity.Id, default), Times.Once);

            entity.Id.Should().Be(response.Result.Id);
        }
    }
}
