using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class AddAssetSummaryUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly AddAssetSummaryUseCase _useCase;

        public AddAssetSummaryUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _useCase = new AddAssetSummaryUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            AddAssetSummaryRequest assetModel = null;

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<AssetSummary>()))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.ExecuteAsync(assetModel).ConfigureAwait(false);

                Assert.True(false, "ArgumentNullException should be thrown!");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().Be("Value cannot be null. (Parameter 'assetSummary')");
            }
        }

        [Fact]
        public async Task Add_ValidModel_CallsGateway()
        {
            AddAssetSummaryRequest assetModel = new AddAssetSummaryRequest();

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<AssetSummary>()))
                .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(assetModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddAsync(It.IsAny<AssetSummary>()), Times.Once);
        }
    }
}
