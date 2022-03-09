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
    public class GetAllAssetSummariesUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetAllAssetSummariesUseCase _getAllAssetSummariesUseCase;

        public GetAllAssetSummariesUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _getAllAssetSummariesUseCase = new GetAllAssetSummariesUseCase(_mockFinanceGateway.Object);
        }

        [Fact]
        public async Task GetAll_WithCustomDate_ReturnsListOfAssetSummaries()
        {
            var assetSummaries = new List<AssetSummary>()
            {
                new AssetSummary
                {
                    Id = new Guid("4e1a96e2-1a51-4e3e-9c13-29ae4fa90bdc"),
                    TargetId = new Guid("00a621fa-c951-43a9-ac4b-9023bb6b97e5"),
                    TargetType = TargetType.Estate,
                    SubmitDate = new DateTime(2021, 7, 2),
                    SummaryYear = 2020,
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
                    SubmitDate = new DateTime(2021, 7, 2),
                    SummaryYear = 2021,
                    AssetName = "Estate 2",
                    TotalDwellingRent = 100,
                    TotalNonDwellingRent = 102,
                    TotalRentalServiceCharge = 20,
                    TotalServiceCharges = 107
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllAssetSummaryAsync(It.IsAny<Guid>(), It.IsAny<DateTime?>())).ReturnsAsync(assetSummaries);

            var expectedResult = new List<AssetSummaryViewResponse>(assetSummaries.ToViewResponse());

            var result = await _getAllAssetSummariesUseCase.ExecuteAsync(assetSummaries[0].TargetId).ConfigureAwait(false);

            result.Should().HaveCount(2);

            assetSummaries[0].SubmitDate.Should().Be(new DateTime(2021, 7, 2));
            assetSummaries[1].SubmitDate.Should().Be(new DateTime(2021, 7, 2));

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAll_WithDefaultDate_ReturnsListOfAssetSummaries()
        {
            var assetSummaries = new List<AssetSummary>()
            {
                new AssetSummary
                {
                    Id = new Guid("4e1a96e2-1a51-4e3e-9c13-29ae4fa90bdc"),
                    TargetId = new Guid("00a621fa-c951-43a9-ac4b-9023bb6b97e5"),
                    TargetType = TargetType.Estate,
                    SubmitDate = DateTime.UtcNow.Date,
                    SummaryYear = 2020,
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
                    SubmitDate = DateTime.UtcNow.Date,
                    SummaryYear = 2021,
                    AssetName = "Estate 2",
                    TotalDwellingRent = 100,
                    TotalNonDwellingRent = 102,
                    TotalRentalServiceCharge = 20,
                    TotalServiceCharges = 107
                }
            };

            _mockFinanceGateway.Setup(x => x.GetAllAssetSummaryAsync(It.IsAny<Guid>(), It.IsAny<DateTime?>())).ReturnsAsync(assetSummaries);

            var expectedResult = new List<AssetSummaryViewResponse>(assetSummaries.ToViewResponse());

            var result = await _getAllAssetSummariesUseCase.ExecuteAsync(assetSummaries[0].TargetId).ConfigureAwait(false);

            result.Should().HaveCount(2);

            assetSummaries[0].SubmitDate.Should().Be(DateTime.UtcNow.Date);
            assetSummaries[1].SubmitDate.Should().Be(DateTime.UtcNow.Date);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
