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
    public class AddStatementUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly AddStatementUseCase _useCase;

        public AddStatementUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _useCase = new AddStatementUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            AddStatementRequest statementModel = null;
            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<Statement>()))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.ExecuteAsync(statementModel).ConfigureAwait(false);
                Assert.True(false, "ArgumentNullException should be thrown!");
            }
            catch(Exception ex)
            {
                ex.Should().NotBeNull();
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().Be("Value cannot be null. (Parameter 'statement')");
            }
        }


        [Fact]
        public async Task Add_ValidModel_CallsGateway()
        {
            AddStatementRequest statementModel = new AddStatementRequest();

            _mockFinanceGateway.Setup(_ => _.AddAsync(It.IsAny<Statement>()))
               .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(statementModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddAsync(It.IsAny<Statement>()), Times.Once);
        }
    }
}
