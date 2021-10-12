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
    public class GetAllRentGroupSummariesUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetAllRentGroupSummariesUseCase _getAllRentGroupSummariesUseCase;

        public GetAllRentGroupSummariesUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _getAllRentGroupSummariesUseCase = new GetAllRentGroupSummariesUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task GetAll_WithCustomDate_ReturnsListOfRentGroupSummaries()
        {
            var rentGroupSummaries = new List<RentGroupSummary>()
            {
                new RentGroupSummary
                {
                    Id = new Guid("c4b19453-4e1c-40ba-b937-db41a46eca45"),
                    RentGroupName = "LeaseHolders",
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    SubmitDate = new DateTime(2021, 7, 2),
                    ChargedYTD = 102,
                    PaidYTD = 160,
                    TotalBalance = -42,
                    TotalCharged = 102,
                    TotalPaid = 160
                },
                new RentGroupSummary
                {
                    Id = new Guid("814ab48c-090e-42fc-9e17-67bf6b3eb48f"),
                    RentGroupName = "LeaseHolders2",
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    SubmitDate = new DateTime(2021, 7, 2),
                    ChargedYTD = 102,
                    PaidYTD = 160,
                    TotalBalance = -42,
                    TotalCharged = 102,
                    TotalPaid = 160
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllRentGroupSummaryAsync(It.IsAny<DateTime>())).ReturnsAsync(rentGroupSummaries);

            var expectedResult = new List<RentGroupSummaryResponse>(rentGroupSummaries.ToResponse());

            var result = await _getAllRentGroupSummariesUseCase.ExecuteAsync(new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAll_WithDefaultDate_ReturnsListOfRentGroupSummaries()
        {
            var rentGroupSummaries = new List<RentGroupSummary>()
            {
                new RentGroupSummary
                {
                    Id = new Guid("c4b19453-4e1c-40ba-b937-db41a46eca45"),
                    RentGroupName = "LeaseHolders",
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    SubmitDate = DateTime.UtcNow.Date,
                    ChargedYTD = 102,
                    PaidYTD = 160,
                    TotalBalance = -42,
                    TotalCharged = 102,
                    TotalPaid = 160
                },
                new RentGroupSummary
                {
                    Id = new Guid("814ab48c-090e-42fc-9e17-67bf6b3eb48f"),
                    RentGroupName = "LeaseHolders2",
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    SubmitDate = DateTime.UtcNow.Date,
                    ChargedYTD = 102,
                    PaidYTD = 160,
                    TotalBalance = -42,
                    TotalCharged = 102,
                    TotalPaid = 160
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllRentGroupSummaryAsync(It.IsAny<DateTime>())).ReturnsAsync(rentGroupSummaries);

            var exprectedResult = new List<RentGroupSummaryResponse>(rentGroupSummaries.ToResponse());

            var result = await _getAllRentGroupSummariesUseCase.ExecuteAsync(new DateTime()).ConfigureAwait(false);

            result.Should().BeEquivalentTo(exprectedResult);
        }
    }
}
