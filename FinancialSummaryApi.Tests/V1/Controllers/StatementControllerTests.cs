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
using System.Net;
using System.Threading.Tasks;
using Xunit;


namespace FinancialSummaryApi.Tests.V1.Controllers
{
    public sealed class StatementControllerTests : IDisposable
    {
        private readonly StatementController _statementController;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _httpContext;

        private readonly Mock<IGetStatementListUseCase> _getListUseCase;
        private readonly Mock<IAddStatementUseCase> _addUseCase;

        public StatementControllerTests()
        {
            _getListUseCase = new Mock<IGetStatementListUseCase>();

            _addUseCase = new Mock<IAddStatementUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _statementController = new StatementController(_getListUseCase.Object, _addUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Fact]
        public async Task GetList_WithValidRequestAndAssetId_Returns200()
        {
            _getListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<GetStatementListRequest>()))
                .ReturnsAsync(
                    new StatementListResponse()
                    {
                        Total = 1,
                        Statements = new List<StatementResponse>()
                        {
                            new StatementResponse()
                            {
                                Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                                TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                                TargetType = TargetType.Block,
                                StatementPeriodEndDate = new DateTime(2021, 8, 3),
                                RentAccountNumber = "987654321",
                                Address = "16 Macron Court, E8 1ND",
                                StatementType = StatementType.Leasehold,
                                ChargedAmount = 350,
                                PaidAmount = 600,
                                HousingBenefitAmount = 800,
                                StartBalance = 1100,
                                FinishBalance = 500
                            }
                        }
                    });

            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PageNumber = 1,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 5)
            };

            var result = await _statementController.GetList(string.Empty, string.Empty, new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"), request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var statementList = okResult.Value as StatementListResponse;

            statementList.Should().NotBeNull();

            statementList.Statements.Should().NotBeNull();
            statementList.Total.Should().Be(1);

            statementList.Statements[0].Id.Should().Be(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"));
            statementList.Statements[0].TargetId.Should().Be(new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"));
            statementList.Statements[0].TargetType.Should().Be(TargetType.Block);
            statementList.Statements[0].StatementPeriodEndDate.Should().Be(new DateTime(2021, 8, 3));
            statementList.Statements[0].RentAccountNumber.Should().Be("987654321");
            statementList.Statements[0].Address.Should().Be("16 Macron Court, E8 1ND");
            statementList.Statements[0].StatementType.Should().Be(StatementType.Leasehold);
            statementList.Statements[0].ChargedAmount.Should().Be(350);
            statementList.Statements[0].PaidAmount.Should().Be(600);
            statementList.Statements[0].HousingBenefitAmount.Should().Be(800);
            statementList.Statements[0].StartBalance.Should().Be(1100);
            statementList.Statements[0].FinishBalance.Should().Be(500);
        }

        [Fact]
        public async Task GetList_WithInvalidDateRange_Returns400()
        {
            _getListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<GetStatementListRequest>()))
                .ReturnsAsync(new StatementListResponse());
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PageNumber = 1,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 1)
            };
            var result = await _statementController.GetList(string.Empty, string.Empty, new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"), request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("StartDate can't be greater than EndDate.");

            _getListUseCase.Verify(x => x.ExecuteAsync(new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"), request), Times.Never);
        }

        [Fact]
        public async Task GetList_WithInvalidData_Returns400()
        {
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PageNumber = -1,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 5)
            };
            _statementController.ModelState.AddModelError("PageNumber", "The field PageNumber must be between 1 and 2147483647.");
            var result = await _statementController.GetList(string.Empty, string.Empty, new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"), request).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("The field PageNumber must be between 1 and 2147483647.");

            _getListUseCase.Verify(x => x.ExecuteAsync(new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"), request), Times.Never);
        }

        [Fact]
        public async Task GetList_Returns500()
        {
            _getListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<GetStatementListRequest>()))
                .ThrowsAsync(new Exception("Test exception"));
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PageNumber = 1,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 5)
            };

            try
            {
                var result = await _statementController.GetList(string.Empty, string.Empty, new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), request).ConfigureAwait(false);
                Assert.True(false, "Exception must be thrown!");
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        [Fact]
        public async Task Create_WithValidData_Returns201()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddStatementRequest>()))
                 .ReturnsAsync(new StatementResponse()
                 {
                     Id = new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"),
                     TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                     TargetType = TargetType.Estate,
                     StatementPeriodEndDate = new DateTime(2021, 7, 1),
                     RentAccountNumber = "123456789",
                     Address = "16 Macron Court, E8 1ND",
                     StatementType = StatementType.Leasehold,
                     ChargedAmount = 200,
                     PaidAmount = 500,
                     HousingBenefitAmount = 300,
                     StartBalance = 1000,
                     FinishBalance = 400
                 });

            var request = new AddStatementRequest
            {
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.Estate,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "16 Macron Court, E8 1ND",
                StatementType = StatementType.Leasehold,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = await _statementController.Create(string.Empty, string.Empty, request)
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(request), Times.Once);

            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();

            objectResult.StatusCode.Should().NotBeNull();

            objectResult.StatusCode.Should().Be((int) HttpStatusCode.Created);

            objectResult.Value.Should().NotBeNull();

            var statementResponse = objectResult.Value as StatementResponse;

            statementResponse.Should().NotBeNull();

            statementResponse.Should().BeEquivalentTo(request);
        }

        [Fact]
        public async Task Create_WithSomeEmptyFieldsValidModel_Returns201()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddStatementRequest>()))
                .ReturnsAsync(new StatementResponse()
                {
                    Id = new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"),
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    TargetType = TargetType.Estate,
                    StatementPeriodEndDate = new DateTime(2021, 7, 1),
                    RentAccountNumber = "123456789",
                    Address = "16 Macron Court, E8 1ND",
                    StatementType = StatementType.Leasehold,
                    ChargedAmount = 0,
                    PaidAmount = 0,
                    HousingBenefitAmount = 0,
                    StartBalance = 0,
                    FinishBalance = 0
                });

            var request = new AddStatementRequest
            {
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.Estate,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "16 Macron Court, E8 1ND",
                StatementType = StatementType.Leasehold
            };


            var result = await _statementController.Create(string.Empty, string.Empty, request)
                .ConfigureAwait(false);


            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(request), Times.Once);

            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();

            objectResult.StatusCode.Should().NotBeNull();

            objectResult.StatusCode.Should().Be((int) HttpStatusCode.Created);

            objectResult.Value.Should().NotBeNull();

            var statementResponse = objectResult.Value as StatementResponse;

            statementResponse.Should().NotBeNull();

            statementResponse.Id.Should().Be(new Guid("bae9c9d9-836f-44bc-946f-33cf78584704"));
            statementResponse.TargetId.Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
            statementResponse.TargetType.Should().Be(TargetType.Estate);
            statementResponse.StatementPeriodEndDate.Should().Be(new DateTime(2021, 7, 1));
            statementResponse.RentAccountNumber.Should().Be("123456789");
            statementResponse.Address.Should().Be("16 Macron Court, E8 1ND");
            statementResponse.StatementType.Should().Be(StatementType.Leasehold);
            statementResponse.ChargedAmount.Should().Be(0);
            statementResponse.PaidAmount.Should().Be(0);
            statementResponse.HousingBenefitAmount.Should().Be(0);
            statementResponse.StartBalance.Should().Be(0);
            statementResponse.FinishBalance.Should().Be(0);
        }

        [Fact]
        public async Task Create_WithInvalidData_Returns400()
        {
            var result = await _statementController.Create(string.Empty, string.Empty, null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("Statement model cannot be null");
        }

        [Fact]
        public async Task Create_Returns500()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<AddStatementRequest>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _statementController.Create(string.Empty, string.Empty, new AddStatementRequest { })
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
            _statementController.Dispose();
        }

    }
}
