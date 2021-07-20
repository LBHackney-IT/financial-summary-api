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
    public class AddWeeklySummaryUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly AddWeeklySummaryUseCase _useCase;

        public AddWeeklySummaryUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _useCase = new AddWeeklySummaryUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            AddWeeklySummaryRequest weeklySummaryModel = null;

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<WeeklySummary>()))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.ExecuteAsync(weeklySummaryModel).ConfigureAwait(false);

                Assert.True(false, "ArgumentNullException should be thrown!");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().Be("Value cannot be null. (Parameter 'weeklySummary')");
            }
        }

        [Fact]
        public async Task Add_ValidModel_CallsGateway()
        {
            AddWeeklySummaryRequest weeklySummaryModel = new AddWeeklySummaryRequest();

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<WeeklySummary>()))
                .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(weeklySummaryModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddAsync(It.IsAny<WeeklySummary>()), Times.Once);
        }
    }
}
