using AutoFixture;
using FinancialSummaryApi.V1.Boundary.Response;
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
    public class GetWeeklySummaryByIdUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetWeeklySummaryByIdUseCase _useCase;
        private readonly Fixture _fixture;

        public GetWeeklySummaryByIdUseCaseTests()
        {
            _fixture = new Fixture();
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _useCase = new GetWeeklySummaryByIdUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task GetById_ValidIdWithDefaultDate_ReturnsWeeklySummary()
        {
            var expectedResult = _fixture.Create<WeeklySummary>();
            _mockFinanceGateway.Setup(_ => _.GetWeeklySummaryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(expectedResult);

            var actualResult = await _useCase.ExecuteAsync(Guid.NewGuid(), Guid.NewGuid()).ConfigureAwait(false);

            actualResult.Should().NotBeNull();
            CompareWeeklySummary(actualResult, expectedResult);
        }

        [Fact]
        public async Task GetById_GatewayReturnsNull_ReturnsNull()
        {
            var expectedResult = _fixture.Create<WeeklySummary>();
            _mockFinanceGateway.Setup(_ => _.GetWeeklySummaryByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((WeeklySummary) null);

            var actualResult = await _useCase.ExecuteAsync(Guid.NewGuid(), Guid.NewGuid()).ConfigureAwait(false);

            actualResult.Should().BeNull();
        }

        private static void CompareWeeklySummary(WeeklySummaryResponse model1, WeeklySummary model2)
        {
            model1.Should().NotBeNull();
            model2.Should().NotBeNull();

            model1.Id.Should().Be(model2.Id);
            model1.TargetId.Should().Be(model2.TargetId);
            model1.PeriodNo.Should().Be(model2.PeriodNo);
            model1.FinancialMonth.Should().Be(model2.FinancialMonth);
            model1.FinancialYear.Should().Be(model2.FinancialYear);
            model1.ChargedAmount.Should().Be(model2.ChargedAmount);
            model1.PaidAmount.Should().Be(model2.PaidAmount);
            model1.BalanceAmount.Should().Be(model2.BalanceAmount);
            model1.HousingBenefitAmount.Should().Be(model2.HousingBenefitAmount);
        }
    }
}
