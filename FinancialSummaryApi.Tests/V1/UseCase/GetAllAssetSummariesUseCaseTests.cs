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
    public class GetAllAssetSummariesUseCaseTests
    {
        private Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private GetAllAssetSummariesUseCase _getAllAssetSummariesUseCase;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _getAllAssetSummariesUseCase = new GetAllAssetSummariesUseCase(_mockFinanceGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllAssetSummariesFromTheGatewayWithDate()
        {
            var assetSummaries = new List<AssetSummary>()
            {
                new AssetSummary
                {
                    Id = new Guid("4e1a96e2-1a51-4e3e-9c13-29ae4fa90bdc"),
                    TargetId = new Guid("00a621fa-c951-43a9-ac4b-9023bb6b97e5"),
                    TargetType = TargetType.Estate,
                    SubmitDate = new DateTime(2021, 7, 2),
                    AssetName = "Estate 1",
                    TotalDwellingRent = 100,
                    TotalNonDwellingRent = 102,
                    TotalRentalServiceCharge = 20,
                    TotalServiceCharges = 107
                },
                new AssetSummary
                {
                    Id = new Guid("5a0127f7-a9e5-4a50-aff6-d8f9eccc1fd3"),
                    TargetId = new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"),
                    TargetType = TargetType.Estate,
                    SubmitDate = new DateTime(2021, 7, 3),
                    AssetName = "Estate 2",
                    TotalDwellingRent = 100,
                    TotalNonDwellingRent = 102,
                    TotalRentalServiceCharge = 20,
                    TotalServiceCharges = 107
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllAssetSummaryAsync(It.IsAny<DateTime>())).ReturnsAsync(assetSummaries);

            var response = new List<AssetSummaryResponse>(assetSummaries.ToResponse());

            var result = await _getAllAssetSummariesUseCase.ExecuteAsync(new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().HaveCount(1);

            result.Should().BeEquivalentTo(response);
        }

        [Test]
        public async Task GetAllAssetSummariesFromTheGatewayWithDefaultDate()
        {
            var assetSummaries = _fixture.CreateMany<AssetSummary>().ToList();
            _mockFinanceGateway.Setup(x => x.GetAllAssetSummaryAsync(It.IsAny<DateTime>())).ReturnsAsync(assetSummaries);

            var response = new List<AssetSummaryResponse>(assetSummaries.ToResponse());

            var result = await _getAllAssetSummariesUseCase.ExecuteAsync(new DateTime()).ConfigureAwait(false);

            result.Should().BeEquivalentTo(response);
        }
    }
}
