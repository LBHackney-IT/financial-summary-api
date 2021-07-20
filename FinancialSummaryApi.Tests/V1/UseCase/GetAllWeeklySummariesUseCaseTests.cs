using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
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
    public class GetAllWeeklySummariesUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetAllWeeklySummariesUseCase _getAllWeeklySummariesUseCase;

        public GetAllWeeklySummariesUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _getAllWeeklySummariesUseCase = new GetAllWeeklySummariesUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task GetAll_WithCustomDate_ReturnsListOfAssetSummaries()
        {
            var weeklySummaries = new List<WeeklySummary>()
            {
                new WeeklySummary
                {
                    Id = new Guid("4e1a96e2-1a51-4e3e-9c13-29ae4fa90bdc"),
                    TargetId = new Guid("00a621fa-c951-43a9-ac4b-9023bb6b97e5"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    WeekStartDate = new DateTime(2021, 7, 2),
                    PeriodNo = 1,
                    BalanceAmount = 120,
                    ChargedAmount = 250,
                    PaidAmount = 120,
                    HousingBenefitAmount = 10
                },
                new WeeklySummary
                {
                    Id = new Guid("5a0127f7-a9e5-4a50-aff6-d8f9eccc1fd3"),
                    TargetId = new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    WeekStartDate = new DateTime(2021, 7, 12),
                    PeriodNo = 2,
                    BalanceAmount = 20,
                    ChargedAmount = 150,
                    PaidAmount = 120,
                    HousingBenefitAmount = 10
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllWeeklySummaryAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(weeklySummaries);

            var expectedResult = new List<WeeklySummaryResponse>(weeklySummaries.ToResponse());

            var result = await _getAllWeeklySummariesUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), "2021-6-2", "2021-7-30").ConfigureAwait(false);

            result.Should().HaveCount(2);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAll_WithNullDate_ReturnsListOfAssetSummaries()
        {
            var weeklySummaries = new List<WeeklySummary>()
            {
               new WeeklySummary
                {
                    Id = new Guid("4e1a96e2-1a51-4e3e-9c13-29ae4fa90bdc"),
                    TargetId = new Guid("00a621fa-c951-43a9-ac4b-9023bb6b97e5"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    WeekStartDate = new DateTime(2021, 6, 2),
                    PeriodNo = 1,
                    BalanceAmount = 120,
                    ChargedAmount = 250,
                    PaidAmount = 120,
                    HousingBenefitAmount = 10
                },
                new WeeklySummary
                {
                    Id = new Guid("5a0127f7-a9e5-4a50-aff6-d8f9eccc1fd3"),
                    TargetId = new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    WeekStartDate = new DateTime(2021, 7, 2),
                    PeriodNo = 2,
                    BalanceAmount = 20,
                    ChargedAmount = 150,
                    PaidAmount = 120,
                    HousingBenefitAmount = 10
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllWeeklySummaryAsync(It.IsAny<Guid>(), null, null)).ReturnsAsync(weeklySummaries);

            var expectedResult = new List<WeeklySummaryResponse>(weeklySummaries.ToResponse());

            var result = await _getAllWeeklySummariesUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), null, null).ConfigureAwait(false);

            result.Should().HaveCount(2);

            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async Task GetAll_WithInvalidDate_ReturnsListOfAssetSummaries()
        {
            var weeklySummaries = new List<WeeklySummary>()
            {
               new WeeklySummary
                {
                    Id = new Guid("4e1a96e2-1a51-4e3e-9c13-29ae4fa90bdc"),
                    TargetId = new Guid("00a621fa-c951-43a9-ac4b-9023bb6b97e5"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    WeekStartDate = new DateTime(2021, 6, 2),
                    PeriodNo = 1,
                    BalanceAmount = 120,
                    ChargedAmount = 250,
                    PaidAmount = 120,
                    HousingBenefitAmount = 10
                },
                new WeeklySummary
                {
                    Id = new Guid("5a0127f7-a9e5-4a50-aff6-d8f9eccc1fd3"),
                    TargetId = new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    WeekStartDate = new DateTime(2021, 7, 2),
                    PeriodNo = 2,
                    BalanceAmount = 20,
                    ChargedAmount = 150,
                    PaidAmount = 120,
                    HousingBenefitAmount = 10
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllWeeklySummaryAsync(It.IsAny<Guid>(), null, null)).ReturnsAsync(weeklySummaries);

            var expectedResult = new List<WeeklySummaryResponse>(weeklySummaries.ToResponse());

            var result = await _getAllWeeklySummariesUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), "invaliddate", "12345").ConfigureAwait(false);

            result.Should().HaveCount(2);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
