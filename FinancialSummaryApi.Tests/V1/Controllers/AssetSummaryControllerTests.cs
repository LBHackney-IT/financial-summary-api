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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Controllers
{
    public sealed class AssetSummaryControllerTests : IDisposable
    {
        private readonly AssetSummaryController _assetSummaryController;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _httpContext;

        private readonly Mock<IGetAllAssetSummariesUseCase> _getAllUseCase;
        private readonly Mock<IGetAssetSummaryByIdUseCase> _getByIdUseCase;
        private readonly Mock<IGetAssetSummaryByIdAndYearUseCase> _mockGetByIdAndYearUseCase;
        private readonly Mock<IAddAssetSummaryUseCase> _addUseCase;
        private readonly Mock<IUpdateAssetSummaryUseCase> _updateAssetSummaryUseCase;

        public AssetSummaryControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllAssetSummariesUseCase>();

            _getByIdUseCase = new Mock<IGetAssetSummaryByIdUseCase>();

            _mockGetByIdAndYearUseCase = new Mock<IGetAssetSummaryByIdAndYearUseCase>();

            _addUseCase = new Mock<IAddAssetSummaryUseCase>();

            _updateAssetSummaryUseCase = new Mock<IUpdateAssetSummaryUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _assetSummaryController = new AssetSummaryController(_getAllUseCase.Object, _getByIdUseCase.Object, _mockGetByIdAndYearUseCase.Object,
                _addUseCase.Object,
                _updateAssetSummaryUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Fact]
        public async Task GetAllByDateAssetSummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new List<AssetSummaryViewResponse>()
                    {
                        new AssetSummaryViewResponse
                        {
                            Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                            TargetId = new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"),
                            Ownership = new OwnershipResponse
                            {
                                TotalLeaseholders = 2,
                                TotalFreeholders = 2,
                                TotalDwellings = 2
                            },
                            SummaryYear = 2020,
                            Type = ValuesType.Actual,
                            TotalServiceCharges = 140
                        }
                    });

            var result = await _assetSummaryController.GetAll(string.Empty, string.Empty, Guid.Empty, new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var assetSummaries = okResult.Value as List<AssetSummaryViewResponse>;

            assetSummaries.Should().NotBeNull();

            assetSummaries.Should().HaveCount(1);

            assetSummaries.First().Id.Should().Be(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"));
            assetSummaries.First().TargetId.Should().Be(new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"));
            assetSummaries.First().TotalServiceCharges.Should().Be(140);
            assetSummaries.First().Ownership.TotalDwellings.Should().Be(2);
            assetSummaries.First().Ownership.TotalFreeholders.Should().Be(2);
            assetSummaries.First().Ownership.TotalLeaseholders.Should().Be(2);
        }

        [Fact]
        public async Task GetAllByAnotherDateAssetSummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<AssetSummaryViewResponse> { });

            var result = await _assetSummaryController.GetAll(string.Empty, string.Empty, Guid.Empty, new DateTime(2021, 7, 1)).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var assetSummaries = okResult.Value as List<AssetSummaryViewResponse>;

            assetSummaries.Should().NotBeNull();

            assetSummaries.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetByAssetIdValidIdReturns200()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
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
                   TotalServiceCharges = 109,
                   TotalIncome = 111,
                   TotalExpenditure = 123
               });

            var result = await _assetSummaryController.Get(string.Empty, string.Empty, new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"), new DateTime(2021, 7, 1))
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
            assetSummary.TotalIncome.Should().Be(111);
            assetSummary.TotalExpenditure.Should().Be(123);
        }

        [Fact]
        public async Task GetByAssetIdAndDateWithInvalidIdReturns404()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync((AssetSummaryResponse) null);

            var result = await _assetSummaryController.Get(string.Empty, string.Empty, new Guid("ff353355-d884-4bc9-a684-f0ccc616ba4e"), new DateTime(2021, 6, 30))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            response.Message.Should().Be("No Asset Summary by provided assetId cannot be found!");

            response.Details.Should().Be("");
        }

        [Fact]
        public async Task GetByAssetIdAndDateReturns500()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _assetSummaryController.Get(string.Empty, string.Empty, new Guid("6791051d-961d-4e16-9853-6e7e45b01b49"), new DateTime(2021, 6, 30))
                    .ConfigureAwait(false);
                Assert.True(false, "Exception must be thrown!");
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        [Fact]
        public async Task CreateAssetSummaryWithValidDataReturns201()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddAssetSummaryRequest>()))
                .ReturnsAsync(new AssetSummaryResponse()
                {
                    Id = new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"),
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    TargetType = TargetType.Estate,
                    AssetName = "Estate 2",
                    SubmitDate = new DateTime(2021, 7, 1),
                    TotalDwellingRent = 87,
                    TotalNonDwellingRent = 37,
                    TotalRentalServiceCharge = 99,
                    TotalServiceCharges = 109,
                    TotalIncome = 111,
                    TotalExpenditure = 123
                });

            var request = new AddAssetSummaryRequest
            {
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.Estate,
                AssetName = "Estate 2",
                SubmitDate = new DateTime(2021, 7, 1),
                TotalDwellingRent = 87,
                TotalNonDwellingRent = 37,
                TotalRentalServiceCharge = 99,
                TotalServiceCharges = 109,
                TotalIncome = 111,
                TotalExpenditure = 123
            };

            var result = await _assetSummaryController.Create(string.Empty, string.Empty, request)
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(request), Times.Once);

            var createdAtActionResult = result as CreatedAtActionResult;

            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult.ActionName.Should().BeEquivalentTo("Get");

            createdAtActionResult.RouteValues["assetId"].Should().NotBeNull();

            createdAtActionResult.RouteValues["assetId"].Should().BeOfType(typeof(Guid));

            createdAtActionResult.Value.Should().NotBeNull();

            var assetResponse = createdAtActionResult.Value as AssetSummaryResponse;

            assetResponse.Should().NotBeNull();

            assetResponse.Should().BeEquivalentTo(request);
        }

        [Fact]
        public async Task CreateAssetSummaryWithSomeEmptyFieldsValidModelReturns201()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddAssetSummaryRequest>()))
                .ReturnsAsync(new AssetSummaryResponse()
                {
                    Id = new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"),
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    TargetType = TargetType.Estate,
                    AssetName = "Estate 2",
                    SubmitDate = new DateTime(2021, 7, 1),
                    TotalDwellingRent = 0,
                    TotalNonDwellingRent = 0,
                    TotalRentalServiceCharge = 0,
                    TotalServiceCharges = 0,
                    TotalIncome = 0,
                    TotalExpenditure = 0
                });

            var request = new AddAssetSummaryRequest
            {
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.Estate,
                AssetName = "Estate 2",
                SubmitDate = new DateTime(2021, 7, 1)
            };

            var result = await _assetSummaryController.Create(string.Empty, string.Empty, request)
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(request), Times.Once);

            var createdAtActionResult = result as CreatedAtActionResult;

            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult.ActionName.Should().BeEquivalentTo("Get");

            createdAtActionResult.RouteValues["assetId"].Should().NotBeNull();

            createdAtActionResult.RouteValues["assetId"].Should().BeOfType(typeof(Guid));

            createdAtActionResult.Value.Should().NotBeNull();

            var assetResponse = createdAtActionResult.Value as AssetSummaryResponse;

            assetResponse.Should().NotBeNull();

            assetResponse.Id.Should().Be(new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"));
            assetResponse.TargetId.Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
            assetResponse.TargetType.Should().Be(TargetType.Estate);
            assetResponse.AssetName.Should().Be("Estate 2");
            assetResponse.SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            assetResponse.TotalDwellingRent.Should().Be(0);
            assetResponse.TotalNonDwellingRent.Should().Be(0);
            assetResponse.TotalRentalServiceCharge.Should().Be(0);
            assetResponse.TotalServiceCharges.Should().Be(0);
            assetResponse.TotalIncome.Should().Be(0);
            assetResponse.TotalExpenditure.Should().Be(0);
        }

        [Fact]
        public async Task CreateAssetSummaryWithInvalidDataReturns400()
        {
            var result = await _assetSummaryController.Create(string.Empty, string.Empty, null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("AssetSummary model cannot be null");
        }

        [Fact]
        public async Task CreateAssetSummaryReturns500()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddAssetSummaryRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _assetSummaryController.Create(string.Empty, string.Empty, new AddAssetSummaryRequest { })
                    .ConfigureAwait(false);
                Assert.True(false, "Exception must be thrown!");
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        public void Dispose()
        {
            _assetSummaryController.Dispose();
        }
    }
}
