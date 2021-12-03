using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AutoFixture;
using AutoMapper;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public class DynamoDbGatewayTests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _mockDynamoDb;
        private readonly DynamoDbGateway _gateway;
        private static IMapper _mapper;

        private readonly List<Action> _cleanup = new List<Action>();
        public DynamoDbGatewayTests()
        {
            _mockDynamoDb = new Mock<IDynamoDBContext>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                    mc.AddProfile(new MappingProfile()));
                _mapper = mappingConfig.CreateMapper();
            }
            _gateway = new DynamoDbGateway(_mockDynamoDb.Object, _mapper);


        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                foreach (var action in _cleanup)
                    action();

                _disposed = true;
            }
        }

        #region assets

        [Fact]
        public async Task AddAssetSummaryWithValidObject()
        {
            _mockDynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<AssetSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _mockDynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddAssetSummaryWithInvalidObject()
        {
            _mockDynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((AssetSummary) null).ConfigureAwait(false);

            _mockDynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), default), Times.Once);
        }
        #endregion

        #region RentGroups

        [Fact]
        public async Task AddRentGroupSummaryWithValidObject()
        {
            _mockDynamoDb.Setup(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<RentGroupSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _mockDynamoDb.Verify(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddRentGroupSummaryWithInvalidObject()
        {
            _mockDynamoDb.Setup(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((RentGroupSummary) null).ConfigureAwait(false);

            _mockDynamoDb.Verify(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), default), Times.Once);
        }
        #endregion

        #region WeeklySummaries

        [Fact]
        public async Task AddWeeklySummaryWithValidObject()
        {
            _mockDynamoDb.Setup(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<WeeklySummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _mockDynamoDb.Verify(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddWeeklySummaryWithInvalidObject()
        {
            _mockDynamoDb.Setup(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((WeeklySummary) null).ConfigureAwait(false);

            _mockDynamoDb.Verify(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), default), Times.Once);
        }
        #endregion


    }
}
