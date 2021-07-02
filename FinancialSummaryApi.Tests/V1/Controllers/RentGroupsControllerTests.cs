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
using System.Net;
using System.Threading.Tasks;

namespace FinancialSummaryApi.Tests.V1.Controllers
{
    [TestFixture]
    public class RentGroupsControllerTests
    {
        private RentGroupsController _rentGroupsController;
        private ControllerContext _controllerContext;
        private HttpContext _httpContext;

        private Mock<IGetAllRentGroupSummariesUseCase> _getAllUseCase;
        private Mock<IGetRentGroupSummaryByNameUseCase> _getByIdUseCase;
        private Mock<IAddRentGroupSummaryUseCase> _addUseCase;

        [SetUp]
        public void Init()
        {
            _getAllUseCase = new Mock<IGetAllRentGroupSummariesUseCase>();

            _getByIdUseCase = new Mock<IGetRentGroupSummaryByNameUseCase>();

            _addUseCase = new Mock<IAddRentGroupSummaryUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _rentGroupsController = new RentGroupsController(_addUseCase.Object, _getByIdUseCase.Object, _getAllUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Test]
        public async Task GetAllByDateRentGroupSummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(
                    new List<RentGroupSummaryResponse>()
                    {
                        new RentGroupSummaryResponse
                        {
                            Id = new Guid("a264d9c1-d419-463e-9df7-e82dff2b9539"),
                            TargetType = TargetType.RentGroup,
                            RentGroupName = "LeaseHolders",
                            SubmitDate = new DateTime(2021, 7, 2),
                            TargetDescription = "desc",
                            ArrearsYTD = 100,
                            ChargedYTD = 120,
                            PaidYTD = 0,
                            TotalCharged = 220,
                            TotalPaid = 0,
                            TotalBalance = -220
                        }
                    });

            var result = await _rentGroupsController.GetAll("", new DateTime(2021, 7, 2)).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var rentGroupSummaries = okResult.Value as List<RentGroupSummaryResponse>;

            rentGroupSummaries.Should().NotBeNull();

            rentGroupSummaries.Should().HaveCount(1);

            rentGroupSummaries[0].Id.Should().Be(new Guid("a264d9c1-d419-463e-9df7-e82dff2b9539"));
            rentGroupSummaries[0].TargetType.Should().Be(TargetType.RentGroup);
            rentGroupSummaries[0].RentGroupName.Should().Be("LeaseHolders");
            rentGroupSummaries[0].SubmitDate.Should().Be(new DateTime(2021, 7, 2));
            rentGroupSummaries[0].TargetDescription.Should().Be("desc");
            rentGroupSummaries[0].ArrearsYTD.Should().Be(100);
            rentGroupSummaries[0].ChargedYTD.Should().Be(120);
            rentGroupSummaries[0].PaidYTD.Should().Be(0);
            rentGroupSummaries[0].TotalCharged.Should().Be(220);
            rentGroupSummaries[0].TotalPaid.Should().Be(0);
            rentGroupSummaries[0].TotalBalance.Should().Be(-220);
        }

        [Test]
        public async Task GetAllByAnotherDateRentGroupSummaryObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(
                    new List<RentGroupSummaryResponse>()
                    {
                        new RentGroupSummaryResponse
                        {
                            Id = new Guid("a264d9c1-d419-463e-9df7-e82dff2b9539"),
                            TargetType = TargetType.RentGroup,
                            RentGroupName = "LeaseHolders",
                            SubmitDate = new DateTime(2021, 7, 2),
                            TargetDescription = "desc",
                            ArrearsYTD = 100,
                            ChargedYTD = 120,
                            PaidYTD = 0,
                            TotalCharged = 220,
                            TotalPaid = 0,
                            TotalBalance = -220
                        }
                    });

            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(new List<RentGroupSummaryResponse> { });

            var result = await _rentGroupsController.GetAll("", new DateTime(2021, 7, 1)).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var rentGroupSummaries = okResult.Value as List<RentGroupSummaryResponse>;

            rentGroupSummaries.Should().NotBeNull();

            rentGroupSummaries.Should().HaveCount(0);
        }

        [Test]
        public async Task GetAllByDateRentGroupSummaryObjectsReturns500()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<DateTime>()))
                .Throws(new Exception("Test exception"));

            try
            {
                var result = await _rentGroupsController.GetAll("", new DateTime(2021, 7, 2))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        [Test]
        public async Task GetByRentGroupNameValidNameReturns200()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new RentGroupSummaryResponse
                {
                    Id = new Guid("a264d9c1-d419-463e-9df7-e82dff2b9539"),
                    TargetType = TargetType.RentGroup,
                    RentGroupName = "LeaseHolders",
                    SubmitDate = new DateTime(2021, 7, 2),
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    ChargedYTD = 120,
                    PaidYTD = 0,
                    TotalCharged = 220,
                    TotalPaid = 0,
                    TotalBalance = -220
                });

            var result = await _rentGroupsController.Get("", "LeaseHolders", new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var rentGroupSummary = okResult.Value as RentGroupSummaryResponse;

            rentGroupSummary.Should().NotBeNull();

            rentGroupSummary.Id.Should().Be(new Guid("a264d9c1-d419-463e-9df7-e82dff2b9539"));
            rentGroupSummary.TargetType.Should().Be(TargetType.RentGroup);
            rentGroupSummary.RentGroupName.Should().Be("LeaseHolders");
            rentGroupSummary.SubmitDate.Should().Be(new DateTime(2021, 7, 2));
            rentGroupSummary.TargetDescription.Should().Be("desc");
            rentGroupSummary.ArrearsYTD.Should().Be(100);
            rentGroupSummary.ChargedYTD.Should().Be(120);
            rentGroupSummary.PaidYTD.Should().Be(0);
            rentGroupSummary.TotalCharged.Should().Be(220);
            rentGroupSummary.TotalPaid.Should().Be(0);
            rentGroupSummary.TotalBalance.Should().Be(-220);
        }

        [Test]
        public async Task GetByRentGroupNameAndDateWithInvalidNameReturns404()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync((RentGroupSummaryResponse) null);

            var result = await _rentGroupsController.Get("", "LeaseHolder", new DateTime(2021, 6, 30))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            response.Message.Should().Be("Rent Group with provided name cannot be found!");

            response.Details.Should().Be("");
        }

        [Test]
        public async Task GetByRentGroupNameAndDateReturns500()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _rentGroupsController.Get("", "LeaseHolders", new DateTime(2021, 6, 30))
                    .ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        [Test]
        public async Task CreateRentGroupSummaryWithValidDataReturns200()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddRentGroupSummaryRequest>()))
                .Returns(Task.CompletedTask);

            var result = await _rentGroupsController.Create("",
                new AddRentGroupSummaryRequest
                {
                    TargetType = TargetType.RentGroup,
                    RentGroupName = "LeaseHolders",
                    SubmitDate = new DateTime(2021, 7, 2),
                    TargetDescription = "desc",
                    ArrearsYTD = 100,
                    ChargedYTD = 120,
                    PaidYTD = 0,
                    TotalCharged = 220,
                    TotalPaid = 0,
                    TotalBalance = -220
                }).ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(It.IsAny<AddRentGroupSummaryRequest>()), Times.Once);

            var redirectToActionResult = result as RedirectToActionResult;

            redirectToActionResult.Should().NotBeNull();

            redirectToActionResult.ActionName.Should().Be("Get");

            redirectToActionResult.RouteValues.Should().NotBeNull();

            redirectToActionResult.RouteValues.Should().HaveCount(1);

            redirectToActionResult.RouteValues["rentGroupName"].Should().Be("LeaseHolders");
        }

        [Test]
        public async Task CreateRentGroupSummaryWithInvalidDataReturns400()
        {
            var result = await _rentGroupsController.Create("", null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("Rent Group Summary model cannot be null");
        }

        [Test]
        public async Task CreateRentGroupSummaryReturns500()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddRentGroupSummaryRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _rentGroupsController.Create("", new AddRentGroupSummaryRequest { })
                    .ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }
    }
}
