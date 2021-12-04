using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Controllers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Controllers
{
    public sealed class WeeklySummaryControllerTests : IDisposable
    {
        private readonly WeeklySummaryController _weeklySummaryController;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _httpContext;

        private readonly Mock<IGetAllWeeklySummariesUseCase> _getAllUseCase;
        private readonly Mock<IGetWeeklySummaryByIdUseCase> _getByIdUseCase;
        private readonly Mock<IAddWeeklySummaryUseCase> _addUseCase;

        public WeeklySummaryControllerTests()
        {
            _getAllUseCase = new Mock<IGetAllWeeklySummariesUseCase>();

            _getByIdUseCase = new Mock<IGetWeeklySummaryByIdUseCase>();

            _addUseCase = new Mock<IAddWeeklySummaryUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _weeklySummaryController = new WeeklySummaryController(_getAllUseCase.Object, _addUseCase.Object, _getByIdUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Fact]
        public async Task GetAllByDateWeeklySummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    new List<WeeklySummaryResponse>()
                    {
                        new WeeklySummaryResponse
                        {
                            Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                            TargetId = new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"),
                            FinancialMonth = 7,
                            FinancialYear = 2021,
                            WeekStartDate = new DateTime(2021, 7, 2),
                            PeriodNo = 1,
                            BalanceAmount = 20,
                            ChargedAmount = 150,
                            PaidAmount = 120,
                            HousingBenefitAmount = 10
                        }
                    });

            var result = await _weeklySummaryController.GetAll(string.Empty, string.Empty, Guid.NewGuid(), "2021-06-30", "2021-07-30").ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var weeklySummaries = okResult.Value as List<WeeklySummaryResponse>;

            weeklySummaries.Should().NotBeNull();

            weeklySummaries.Should().HaveCount(1);

            weeklySummaries[0].Id.Should().Be(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"));
            weeklySummaries[0].TargetId.Should().Be(new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"));
            weeklySummaries[0].PeriodNo.Should().Be(1);
            weeklySummaries[0].FinancialYear.Should().Be(2021);
            weeklySummaries[0].FinancialMonth.Should().Be(7);
            weeklySummaries[0].ChargedAmount.Should().Be(150);
            weeklySummaries[0].BalanceAmount.Should().Be(20);
            weeklySummaries[0].PaidAmount.Should().Be(120);
            weeklySummaries[0].HousingBenefitAmount.Should().Be(10);
            weeklySummaries[0].WeekStartDate.Should().Be(new DateTime(2021, 7, 2));
        }

        [Fact]
        public async Task GetAllByWeeklySummaryObjectsReturns500()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _weeklySummaryController.GetAll(string.Empty, string.Empty, Guid.NewGuid(), "2021-06-30", "2021-07-30").ConfigureAwait(false);
                Assert.True(false, "Exception must be thrown!");
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        [Fact]
        public async Task GetByAssetIdValidIdReturns200()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync(new WeeklySummaryResponse
               {
                   Id = new Guid("c5f95fc9-ade5-4b13-96bc-1adff137e246"),
                   TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                   FinancialMonth = 7,
                   FinancialYear = 2021,
                   PeriodNo = 1,
                   WeekStartDate = new DateTime(2021, 7, 1),
                   BalanceAmount = 50,
                   PaidAmount = 37,
                   ChargedAmount = 99,
                   HousingBenefitAmount = 12
               });

            var result = await _weeklySummaryController.Get(string.Empty, string.Empty, new Guid("c5f95fc9-ade5-4b13-96bc-1adff137e246"), new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var weeklySummary = okResult.Value as WeeklySummaryResponse;

            weeklySummary.Should().NotBeNull();

            weeklySummary.Id.Should().Be(new Guid("c5f95fc9-ade5-4b13-96bc-1adff137e246"));
            weeklySummary.TargetId.Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
            weeklySummary.PeriodNo.Should().Be(1);
            weeklySummary.FinancialYear.Should().Be(2021);
            weeklySummary.FinancialMonth.Should().Be(7);
            weeklySummary.WeekStartDate.Should().Be(new DateTime(2021, 7, 1));
            weeklySummary.ChargedAmount.Should().Be(99);
            weeklySummary.BalanceAmount.Should().Be(50);
            weeklySummary.PaidAmount.Should().Be(37);
            weeklySummary.HousingBenefitAmount.Should().Be(12);
        }

        [Fact]
        public async Task GetByAssetIdAndDateWithInvalidIdReturns404()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((WeeklySummaryResponse) null);

            var result = await _weeklySummaryController.Get(string.Empty, string.Empty, new Guid("ff353355-d884-4bc9-a684-f0ccc616ba4e"), new Guid("ff353355-d884-4bc9-a684-f0ccc616ba4e"))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            response.Message.Should().Be("Weekly Summary by provided Id not found!");

            response.Details.Should().Be("");
        }

        [Fact]
        public async Task GetByAssetIdAndDateReturns500()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _weeklySummaryController.Get(string.Empty, string.Empty, new Guid("6791051d-961d-4e16-9853-6e7e45b01b49"), new Guid("6791051d-961d-4e16-9853-6e7e45b01b49"))
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
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddWeeklySummaryRequest>()))
                .ReturnsAsync(new WeeklySummaryResponse()
                {
                    Id = new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"),
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    FinancialMonth = 7,
                    FinancialYear = 2021,
                    PeriodNo = 1,
                    WeekStartDate = new DateTime(2021, 7, 1),
                    BalanceAmount = 50,
                    PaidAmount = 37,
                    ChargedAmount = 99,
                    HousingBenefitAmount = 12,
                    SubmitDate = DateTime.Now
                });

            var request = new AddWeeklySummaryRequest
            {
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                FinancialMonth = 7,
                FinancialYear = 2021,
                PeriodNo = 1,
                WeekStartDate = new DateTime(2021, 7, 1),
                BalanceAmount = 50,
                PaidAmount = 37,
                ChargedAmount = 99,
                HousingBenefitAmount = 12
            };

            var result = await _weeklySummaryController.Create(string.Empty, string.Empty, request)
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(request), Times.Once);

            var createdAtActionResult = result as CreatedAtActionResult;

            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult.ActionName.Should().BeEquivalentTo("Get");

            createdAtActionResult.RouteValues["id"].Should().NotBeNull();

            createdAtActionResult.RouteValues["id"].Should().BeOfType(typeof(Guid));

            createdAtActionResult.Value.Should().NotBeNull();

            var weeklySummaryResponse = createdAtActionResult.Value as WeeklySummaryResponse;

            weeklySummaryResponse.Should().NotBeNull();
            request.Should().BeEquivalentTo(weeklySummaryResponse, opt => opt.Excluding(a => a.Id).Excluding(a => a.SubmitDate));
            weeklySummaryResponse.SubmitDate.Should().BeSameDateAs(DateTime.Now);
        }

        [Fact]
        public async Task CreateWeeklySummaryWithInvalidDataReturns400()
        {
            var result = await _weeklySummaryController.Create(string.Empty, string.Empty, null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("WeeklySummary model cannot be null");
        }

        [Fact]
        public async Task CreateWeeklySummaryReturns500()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddWeeklySummaryRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _weeklySummaryController.Create(string.Empty, string.Empty, new AddWeeklySummaryRequest { })
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
            _weeklySummaryController.Dispose();
        }
    }
}
