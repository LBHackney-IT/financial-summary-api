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
    public class AddRentGroupSummaryUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly AddRentGroupSummaryUseCase _useCase;

        public AddRentGroupSummaryUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _useCase = new AddRentGroupSummaryUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            AddRentGroupSummaryRequest assetModel = null;

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<RentGroupSummary>()))
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
                ex.Message.Should().Be("Value cannot be null. (Parameter 'rentGroupSummary')");
            }
        }

        [Fact]
        public async Task Add_ValidModel_CallsGateway()
        {
            var rentGroupModel = new AddRentGroupSummaryRequest();

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<RentGroupSummary>()))
                .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(rentGroupModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddAsync(It.IsAny<RentGroupSummary>()), Times.Once);
        }
    }
}
