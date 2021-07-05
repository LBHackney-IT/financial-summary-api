using AutoFixture;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class GetAllRentGroupSummariesUseCaseTests
    {
        private Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private GetAllRentGroupSummariesUseCase _getAllRentGroupSummariesUseCase;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _getAllRentGroupSummariesUseCase = new GetAllRentGroupSummariesUseCase(_mockFinanceGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllAssetSummariesFromTheGatewayWithDate()
        {
            var rentGroupSummaries = _fixture.CreateMany<RentGroupSummary>().ToList();

            _mockFinanceGateway.Setup(x => x.GetAllRentGroupSummaryAsync(It.IsAny<DateTime>())).ReturnsAsync(rentGroupSummaries);

            var response = new List<RentGroupSummaryResponse>(rentGroupSummaries.ToResponse());

            var result = await _getAllRentGroupSummariesUseCase.ExecuteAsync(new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().BeEquivalentTo(response);
        }

        [Test]
        public async Task GetAllAssetSummariesFromTheGatewayWithDefaultDate()
        {
            var rentGroupSummaries = _fixture.CreateMany<RentGroupSummary>().ToList();
            _mockFinanceGateway.Setup(x => x.GetAllRentGroupSummaryAsync(It.IsAny<DateTime>())).ReturnsAsync(rentGroupSummaries);

            var response = new List<RentGroupSummaryResponse>(rentGroupSummaries.ToResponse());

            var result = await _getAllRentGroupSummariesUseCase.ExecuteAsync(new DateTime()).ConfigureAwait(false);

            result.Should().BeEquivalentTo(response);
        }
    }
}
