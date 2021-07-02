using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Controllers;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.Controllers
{
    [TestFixture]
    public class AssetSummaryControllerTests
    {
        private AssetSummaryController _assetSummaryController;
        private ControllerContext _controllerContext;
        private HttpContext _httpContext;

        private Mock<IGetAllAssetSummariesUseCase> _getAllUseCase;
        private Mock<IGetAssetSummaryByIdUseCase> _getByIdUseCase;
        private Mock<IAddAssetSummaryUseCase> _addUseCase;

        [SetUp]
        public void Init()
        {
            _getAllUseCase = new Mock<IGetAllAssetSummariesUseCase>();

            _getByIdUseCase = new Mock<IGetAssetSummaryByIdUseCase>();

            _addUseCase = new Mock<IAddAssetSummaryUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _assetSummaryController = new AssetSummaryController(_getAllUseCase.Object, _getByIdUseCase.Object, _addUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Test]
        public async Task GetAllByDateAssetSummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(new DateTime(2021, 7, 2)))
                .ReturnsAsync(
                    new List<AssetSummaryResponse>()
                    {
                        new AssetSummaryResponse
                        {
                            Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                            TargetId = new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"),
                            TargetType = TargetType.Estate,
                            AssetName = "Estate 1",
                            SubmitDate = new DateTime(2021, 7, 2),
                            TotalDwellingRent = 100,
                            TotalNonDwellingRent = 50,
                            TotalRentalServiceCharge = 120,
                            TotalServiceCharges = 140
                        }
                    });

            var result = await _assetSummaryController.GetAll("", new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var assetSummaries = okResult.Value as List<AssetSummaryResponse>;

            assetSummaries.Should().NotBeNull();

            assetSummaries.Should().HaveCount(1);

            assetSummaries[0].Id.Should().Be(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"));
            assetSummaries[0].TargetId.Should().Be(new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"));
            assetSummaries[0].TargetType.Should().Be(TargetType.Estate);
            assetSummaries[0].AssetName.Should().Be("Estate 1");
            assetSummaries[0].SubmitDate.Should().Be(new DateTime(2021, 7, 2));
            assetSummaries[0].TotalDwellingRent.Should().Be(100);
            assetSummaries[0].TotalNonDwellingRent.Should().Be(50);
            assetSummaries[0].TotalRentalServiceCharge.Should().Be(120);
            assetSummaries[0].TotalServiceCharges.Should().Be(140);
        }

        [Test]
        public async Task GetAllByAnotherDateAssetSummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(new DateTime(2021, 7, 2)))
                .ReturnsAsync(
                    new List<AssetSummaryResponse>()
                    {
                        new AssetSummaryResponse
                        {
                            Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                            TargetId = new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"),
                            TargetType = TargetType.Estate,
                            AssetName = "Estate 1",
                            SubmitDate = new DateTime(2021, 7, 2),
                            TotalDwellingRent = 100,
                            TotalNonDwellingRent = 50,
                            TotalRentalServiceCharge = 120,
                            TotalServiceCharges = 140
                        }
                    });

            _getAllUseCase.Setup(x => x.ExecuteAsync(new DateTime(2021, 7, 1)))
                .ReturnsAsync(new List<AssetSummaryResponse> { });

            var result = await _assetSummaryController.GetAll("", new DateTime(2021, 7, 1)).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var assetSummaries = okResult.Value as List<AssetSummaryResponse>;

            assetSummaries.Should().NotBeNull();

            assetSummaries.Should().HaveCount(0);
        }

        [Test]
        public async Task GetAllByDateAssetSummaryObjectsReturns500()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _assetSummaryController.GetAll("", new DateTime(2021, 7, 2)).ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Test exception");
            }
        }

        [Test]
        public async Task GetByAssetIdAndDateWithProvidedIdAssetSummaryObjectReturns200()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"), new DateTime(2021, 7, 1)))
               .ReturnsAsync(new AssetSummaryResponse
               {
                   Id = new Guid("c5f95fc9-ade5-4b13-96bc-1adff137e246"),
                   TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                   TargetType = TargetType.Estate,
                   AssetName = "Estate 2",
                   SubmitDate = new DateTime(2021, 7, 1),
                   TotalDwellingRent = 87,
                   TotalNonDwellingRent = 37,
                   TotalRentalServiceCharge = 99,
                   TotalServiceCharges = 109
               });

            var result = await _assetSummaryController.Get("", new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"), new DateTime(2021, 7, 1))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var assetSummary = okResult.Value as AssetSummaryResponse;

            assetSummary.Should().NotBeNull();

            assetSummary.Id.Should().Be(new Guid("c5f95fc9-ade5-4b13-96bc-1adff137e246"));
            assetSummary.TargetId.Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
            assetSummary.TargetType.Should().Be(TargetType.Estate);
            assetSummary.AssetName.Should().Be("Estate 2");
            assetSummary.SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            assetSummary.TotalDwellingRent.Should().Be(87);
            assetSummary.TotalNonDwellingRent.Should().Be(37);
            assetSummary.TotalRentalServiceCharge.Should().Be(99);
            assetSummary.TotalServiceCharges.Should().Be(109);
        }

        [Test]
        public async Task GetByAssetIdAndDateWithInvalidIdReturns404()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync((AssetSummaryResponse) null);

            var result = await _assetSummaryController.Get("", new Guid("ff353355-d884-4bc9-a684-f0ccc616ba4e"), new DateTime(2021, 6, 30))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.Message.Should().Be("No Asset Summary by provided assetId cannot be found!");
        }

        [Test]
        public async Task GetByAssetIdAndDateReturns500()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _assetSummaryController.Get("", new Guid("6791051d-961d-4e16-9853-6e7e45b01b49"), new DateTime(2021, 6, 30))
                    .ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Test exception");
            }
        }

        [Test]
        public async Task CreateAssetSummaryWithValidDataReturns200()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddAssetSummaryRequest>())).Returns(Task.CompletedTask);

            var result = await _assetSummaryController.Create("",
                new AddAssetSummaryRequest
                {
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    TargetType = TargetType.Estate,
                    AssetName = "Estate 2",
                    SubmitDate = new DateTime(2021, 7, 1),
                    TotalDwellingRent = 87,
                    TotalNonDwellingRent = 37,
                    TotalRentalServiceCharge = 99,
                    TotalServiceCharges = 109
                }).ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(It.IsAny<AddAssetSummaryRequest>()), Times.Once);

            var redirectToActionResult = result as RedirectToActionResult;

            redirectToActionResult.Should().NotBeNull();

            redirectToActionResult.ActionName.Should().Be("Get");

            redirectToActionResult.RouteValues.Should().NotBeNull();

            redirectToActionResult.RouteValues.Should().HaveCount(1);

            redirectToActionResult.RouteValues["assetId"].Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
        }

        [Test]
        public async Task CreateAssetSummaryWithInvalidDataReturns400()
        {
            var result = await _assetSummaryController.Create("", null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.Message.Should().Be("AssetSummary model cannot be null");
        }

        [Test]
        public async Task CreateAssetSummaryReturns500()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddAssetSummaryRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _assetSummaryController.Create("", new AddAssetSummaryRequest { })
                    .ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Test exception");
            }
        }
    }
}
