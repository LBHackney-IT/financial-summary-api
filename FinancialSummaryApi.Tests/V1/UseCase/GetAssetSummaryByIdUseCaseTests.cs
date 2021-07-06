using AutoFixture;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class GetAssetSummaryByIdUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetAssetSummaryByIdUseCase _useCase;
        private readonly Fixture _fixture;

        public GetAssetSummaryByIdUseCaseTests()
        {
            _fixture = new Fixture();
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();
            _useCase = new GetAssetSummaryByIdUseCase(_mockFinanceGateway.Object);
        }

        [Test]
        public async Task GetById_ValidIdWithDefaultDate_ReturnsAsset()
        {
            var expectedResult = _fixture.Create<AssetSummary>();
            _mockFinanceGateway.Setup(_ => _.GetAssetSummaryByIdAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expectedResult);

            var actualResult = await _useCase.ExecuteAsync(Guid.NewGuid(), default).ConfigureAwait(false);

            actualResult.Should().NotBeNull();
            CompareAssetSummary(actualResult, expectedResult);
        }

        [Test]
        public async Task GetById_ValidIdWithProvidedDate_ReturnsAsset()
        {
            var expectedResult = _fixture.Create<AssetSummary>();
            _mockFinanceGateway.Setup(_ => _.GetAssetSummaryByIdAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expectedResult);

            var actualResult = await _useCase.ExecuteAsync(Guid.NewGuid(), new DateTime(2021, 06, 28)).ConfigureAwait(false);

            actualResult.Should().NotBeNull();
            CompareAssetSummary(actualResult, expectedResult);
        }

        [Test]
        public async Task GetById_GatewayReturnsNull_ReturnsNull()
        {
            var expectedResult = _fixture.Create<AssetSummary>();
            _mockFinanceGateway.Setup(_ => _.GetAssetSummaryByIdAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync((AssetSummary) null);

            var actualResult = await _useCase.ExecuteAsync(Guid.NewGuid(), new DateTime(2021, 06, 28)).ConfigureAwait(false);

            actualResult.Should().BeNull();
        }

        private static void CompareAssetSummary(AssetSummaryResponse model1, AssetSummary model2)
        {
            model1.Should().NotBeNull();
            model2.Should().NotBeNull();

            model1.Id.Should().Be(model2.Id);
            model1.TargetId.Should().Be(model2.TargetId);
            model1.TargetType.Should().Be(model2.TargetType);
            model1.AssetName.Should().BeEquivalentTo(model2.AssetName);
            model1.TotalDwellingRent.Should().Be(model2.TotalDwellingRent);
            model1.TotalNonDwellingRent.Should().Be(model2.TotalNonDwellingRent);
            model1.TotalRentalServiceCharge.Should().Be(model2.TotalRentalServiceCharge);
            model1.TotalServiceCharges.Should().Be(model2.TotalServiceCharges);
            model1.SubmitDate.Should().Be(model2.SubmitDate);
        }
    }
}
