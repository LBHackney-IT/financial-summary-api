using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class GetRentGroupSummaryByNameUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetRentGroupSummaryByNameUseCase _getRentGroupSummaryByNameUseCase;

        public GetRentGroupSummaryByNameUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _getRentGroupSummaryByNameUseCase = new GetRentGroupSummaryByNameUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task GetByRentGroupName_WithValidRentGroupNameAndDefaultDate_ReturnsRentGroupSummary()
        {
            var rentGroupSummary = new RentGroupSummary
            {
                Id = new Guid("32d8365a-d0ea-405b-a46e-2e563b043abd"),
                TargetType = TargetType.RentGroup,
                RentGroupName = "LeaseHolder",
                TargetDescription = "desc",
                SubmitDate = DateTime.UtcNow.Date,
                ArrearsYTD = 100,
                ChargedYTD = 120,
                PaidYTD = 120,
                TotalBalance = -100,
                TotalCharged = 220,
                TotalPaid = 120
            };

            _mockFinanceGateway.Setup(_ => _.GetRentGroupSummaryByNameAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(rentGroupSummary);

            var expectedResult = rentGroupSummary.ToResponse();

            var result = await _getRentGroupSummaryByNameUseCase.ExecuteAsync("LeaseHolder", new DateTime()).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByRentGroupName_WithValidRentGroupNameAndCustomDate_ReturnsRentGroupSummary()
        {
            var rentGroupSummary = new RentGroupSummary
            {
                Id = new Guid("32d8365a-d0ea-405b-a46e-2e563b043abd"),
                TargetType = TargetType.RentGroup,
                RentGroupName = "LeaseHolder",
                TargetDescription = "desc",
                SubmitDate = new DateTime(2021, 7, 2),
                ArrearsYTD = 100,
                ChargedYTD = 120,
                PaidYTD = 120,
                TotalBalance = -100,
                TotalCharged = 220,
                TotalPaid = 120
            };

            _mockFinanceGateway.Setup(_ => _.GetRentGroupSummaryByNameAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(rentGroupSummary);

            var expectedResult = rentGroupSummary.ToResponse();

            var result = await _getRentGroupSummaryByNameUseCase.ExecuteAsync("LeaseHolder", new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByRentGroupName_GatewayReturnsNull_ReturnsNull()
        {
            _mockFinanceGateway.Setup(_ => _.GetRentGroupSummaryByNameAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
               .ReturnsAsync((RentGroupSummary) null);

            var result = await _getRentGroupSummaryByNameUseCase.ExecuteAsync("String", new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().BeNull();
        }
    }
}
