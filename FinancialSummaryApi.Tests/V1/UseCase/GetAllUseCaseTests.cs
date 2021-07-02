using AutoFixture;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase;
using Moq;
using NUnit.Framework;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private GetAllAssetSummariesUseCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            _classUnderTest = new GetAllAssetSummariesUseCase(_mockFinanceGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public void GetsAllFromTheGateway()
        {
            //var stubbedEntities = _fixture.CreateMany<Entity>().ToList();
            //_mockFinanceGateway.Setup(x => x.GetAll()).Returns(stubbedEntities);

            //var expectedResponse = new ResponseObjectList { ResponseObjects = stubbedEntities.ToResponse() };

            //_classUnderTest.Execute().Should().BeEquivalentTo(expectedResponse);
        }

        //TODO: Add extra tests here for extra functionality added to the use case
    }
}
