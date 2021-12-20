using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class AddRentGroupSummaryListUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly AddRentGroupSummaryListUseCase _useCase;

        public AddRentGroupSummaryListUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _useCase = new AddRentGroupSummaryListUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            List<AddRentGroupSummaryRequest> rentGroupModel = null;

            _mockFinanceGateway.Setup(_ => _.AddRangeAsync(It.IsAny<List<RentGroupSummary>>()))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.ExecuteAsync(rentGroupModel).ConfigureAwait(false);

                Assert.True(false, "ArgumentNullException should be thrown!");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().Be("Value cannot be null. (Parameter 'summaryRequests')");
            }
        }

        [Fact]
        public async Task Add_ValidModel_CallsGateway()
        {
            var rentGroupModel = new List<AddRentGroupSummaryRequest>
            {
                new AddRentGroupSummaryRequest()
            };

            _mockFinanceGateway.Setup(_ => _.AddRangeAsync(It.IsAny<List<RentGroupSummary>>()))
                .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(rentGroupModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddRangeAsync(It.IsAny<List<RentGroupSummary>>()), Times.Once);
        }
    }
}
