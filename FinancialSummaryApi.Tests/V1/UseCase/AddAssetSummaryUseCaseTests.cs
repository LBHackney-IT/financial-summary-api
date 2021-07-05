using AutoFixture;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class AddAssetSummaryUseCaseTests
    {
        private Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private AddAssetSummaryUseCase _useCase;
        private Fixture _fixture;

        public AddAssetSummaryUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _useCase = new AddAssetSummaryUseCase(_mockFinanceGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            AddAssetSummaryRequest assetModel = null;

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<AssetSummary>()))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.ExecuteAsync(assetModel).ConfigureAwait(false);

                Assert.Fail("ArgumentNullException should be thrown!");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.GetType().Should().Be(typeof(ArgumentNullException));
                ex.Message.Should().Be("Value cannot be null. (Parameter 'assetSummary')");
            }
        }

        [Test]
        public async Task Add_ValidModel_CallsGateway()
        {
            AddAssetSummaryRequest assetModel = _fixture.Create<AddAssetSummaryRequest>();

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<AssetSummary>()))
                .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(assetModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddAsync(It.IsAny<AssetSummary>()), Times.Once);
        }

    }
}
