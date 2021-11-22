using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Controllers;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
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
        private readonly Mock<IAddStatementListUseCase> _addListUseCase;
        private readonly Mock<IExportStatementUseCase> _exportStatementUseCase;

        private readonly IMapper _mapper;

        public StatementControllerTests()
        {
            _getListUseCase = new Mock<IGetStatementListUseCase>();

            _addListUseCase = new Mock<IAddStatementListUseCase>();

            _exportStatementUseCase = new Mock<IExportStatementUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _statementController = new StatementController(_getListUseCase.Object, _addListUseCase.Object, _exportStatementUseCase.Object)
            {
                ControllerContext = _controllerContext
            };

            var mappingConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new MappingProfile()));
            _mapper = mappingConfig.CreateMapper();
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
        public async Task GetList_WithPartialDatesProvided_Returns400()
        {
            _getListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<GetStatementListRequest>()))
                .ReturnsAsync(new StatementListResponse());
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PageNumber = 1,
                StartDate = new DateTime(2021, 8, 3),
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

            response.Message.Should().Be("StartDate and EndDate cannot be partial provided. Dates can be both empty or both provided");

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
            var requestList = new List<AddStatementRequest>
            {
                CreateAddStatementRequest(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"), new DateTime(2021, 7, 1)),
                CreateAddStatementRequest(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0355"), new DateTime(2021, 7, 2))
            };
            var statementsList = _mapper.Map<List<Statement>>(requestList);
            statementsList[0].Id = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
            statementsList[1].Id = new Guid("4fc2872e-5131-4399-8959-c4a17b611f9c");
            var returnedList = _mapper.Map<List<StatementResponse>>(statementsList);

            _addListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<List<AddStatementRequest>>()))
                 .ReturnsAsync(returnedList);

            var result = await _statementController.Create(string.Empty, string.Empty, requestList)
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            _addListUseCase.Verify(x => x.ExecuteAsync(requestList), Times.Once);

            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();

            objectResult.StatusCode.Should().NotBeNull();

            objectResult.StatusCode.Should().Be((int) HttpStatusCode.Created);

            objectResult.Value.Should().NotBeNull();

            var statementResponseList = objectResult.Value as List<StatementResponse>;

            statementResponseList.Should().NotBeNull();

            statementResponseList.Should().BeEquivalentTo(returnedList);
        }

        [Fact]
        public async Task Create_WithSomeEmptyFieldsValidModel_Returns201()
        {
            var requestList = new List<AddStatementRequest>
            {
                 new AddStatementRequest
                {
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    TargetType = TargetType.Estate,
                    StatementPeriodEndDate = new DateTime(2021, 7, 1),
                    RentAccountNumber = "123456789",
                    Address = "16 Macron Court, E8 1ND",
                    StatementType = StatementType.Leasehold
                }
            };
            var statementsList = _mapper.Map<List<Statement>>(requestList);
            statementsList[0].Id = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
            var returnedList = _mapper.Map<List<StatementResponse>>(statementsList);

            _addListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<List<AddStatementRequest>>()))
                .ReturnsAsync(returnedList);

            var result = await _statementController.Create(string.Empty, string.Empty, requestList)
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            _addListUseCase.Verify(x => x.ExecuteAsync(requestList), Times.Once);

            var objectResult = result as ObjectResult;

            objectResult.Should().NotBeNull();

            objectResult.StatusCode.Should().NotBeNull();

            objectResult.StatusCode.Should().Be((int) HttpStatusCode.Created);

            objectResult.Value.Should().NotBeNull();

            var statementResponseList = objectResult.Value as List<StatementResponse>;

            statementResponseList.Should().NotBeNull();

            statementResponseList.Should().BeEquivalentTo(returnedList);
        }

        [Fact]
        public async Task Create_WithNulldData_Returns400()
        {
            var result = await _statementController.Create(string.Empty, string.Empty, null).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("Statement models cannot be null or empty");
        }

        [Fact]
        public async Task Create_WithInvalidData_Returns400()
        {
            var requestList = new List<AddStatementRequest>
            {
                CreateAddStatementRequest(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"), new DateTime(2021, 7, 1)),
                CreateAddStatementRequest(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0355"), new DateTime(2021, 7, 2))
            };
            requestList[0].PaidAmount = -1;

            _statementController.ModelState.AddModelError("PaidAmount", "'Paid Amount' must be greater than or equal to '0'.");

            var result = await _statementController.Create(string.Empty, string.Empty, requestList)
                .ConfigureAwait(false);

            _addListUseCase.Verify(x => x.ExecuteAsync(requestList), Times.Never);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as BaseErrorResponse;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be("");

            response.Message.Should().Be("'Paid Amount' must be greater than or equal to '0'.");
        }

        [Fact]
        public async Task Create_Returns500()
        {
            _addListUseCase.Setup(x => x.ExecuteAsync(It.IsAny<List<AddStatementRequest>>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _statementController.Create(string.Empty, string.Empty, new List<AddStatementRequest> { new AddStatementRequest { } })
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

        private static AddStatementRequest CreateAddStatementRequest(Guid targetId, DateTime statementPeriodEndDate)
        {
            Random rand = new Random();

            var targetTypes = Enum.GetValues(typeof(TargetType));
            var statementTypes = Enum.GetValues(typeof(StatementType));

            return new AddStatementRequest()
            {
                TargetId = targetId,
                TargetType = (TargetType) targetTypes.GetValue(rand.Next(0, targetTypes.Length - 1)),
                StatementPeriodEndDate = statementPeriodEndDate,
                RentAccountNumber = "Some account number",
                Address = "Some address",
                StatementType = (StatementType) statementTypes.GetValue(rand.Next(0, statementTypes.Length - 1)),
                ChargedAmount = (decimal) rand.NextDouble() * 50,
                PaidAmount = (decimal) rand.NextDouble() * 50,
                HousingBenefitAmount = (decimal) rand.NextDouble() * 50,
                StartBalance = (decimal) rand.NextDouble() * 50,
                FinishBalance = (decimal) rand.NextDouble() * 50
            };
        }
    }
}
