using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase;
using Moq;
using NUnit.Framework;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<IFinanceSummaryGateway> _mockGateway;
        private GetAssetSummaryByIdUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IFinanceSummaryGateway>();
            _classUnderTest = new GetAssetSummaryByIdUseCase(_mockGateway.Object);
        }

        //TODO: test to check that the use case retrieves the correct record from the database.
        //Guidance on unit testing and example of mocking can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Writing-Unit-Tests
    }
}
