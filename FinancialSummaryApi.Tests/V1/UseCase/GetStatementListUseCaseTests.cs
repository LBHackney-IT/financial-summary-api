using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.Infrastructure;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Hackney.Core.DynamoDb;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialSummaryApi.V1.Mappings;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class GetStatementListUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly GetStatementListUseCase _getStatementListUseCase;
        private readonly IMapper _mapper;

        public GetStatementListUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                    mc.AddProfile(new MappingProfile()));
                _mapper = mappingConfig.CreateMapper();
            }
            _getStatementListUseCase = new GetStatementListUseCase(_mockFinanceGateway.Object, _mapper);
        }

        [Fact]
        public async Task GetStatementList_WithCustomRequest_ReturnsStatementListResponse()
        {
            var statements = new List<Statement>()
            {
                new Statement
                {
                    Id = new Guid("4983a920-dcac-48e5-90f6-31195a2dcb69"),
                    TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                    TargetType = TargetType.Block,
                    StatementPeriodEndDate = new DateTime(2021, 8, 3),
                    RentAccountNumber = "987654321",
                    Address = "15 Macron Court, E8 1ND",
                    StatementType = StatementType.Tenant,
                    ChargedAmount = 350,
                    PaidAmount = 600,
                    HousingBenefitAmount = 400,
                    StartBalance = 1100,
                    FinishBalance = 500
                },
                new Statement
                {
                    Id = new Guid("5a0127f7-a9e5-4a50-aff6-d8f9eccc1fd3"),
                    TargetId = new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"),
                    TargetType = TargetType.Block,
                    StatementPeriodEndDate = new DateTime(2021, 8, 3),
                    RentAccountNumber = "987654321",
                    Address = "15 Macron Court, E8 1ND",
                    StatementType = StatementType.Tenant,
                    ChargedAmount = 350,
                    PaidAmount = 600,
                    HousingBenefitAmount = 400,
                    StartBalance = 1100,
                    FinishBalance = 500
                },

            };
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PaginationToken = string.Empty,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 5)
            };
            var returnedResult = new PagedResult<Statement>(statements);
            var expectedResult = _mapper.Map<PagedResult<StatementResponse>>(returnedResult);

            _mockFinanceGateway.Setup(x => x.GetPagedStatementsAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnedResult);

            var result = await _getStatementListUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), request).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetStatementList_WithEqualStartAndEndDates_ReturnsStatementListResponse()
        {
            var statements = new List<Statement>()
            {
                new Statement
                {
                    Id = new Guid("4983a920-dcac-48e5-90f6-31195a2dcb69"),
                    TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                    TargetType = TargetType.Block,
                    StatementPeriodEndDate = new DateTime(2021, 8, 3),
                    RentAccountNumber = "987654321",
                    Address = "15 Macron Court, E8 1ND",
                    StatementType = StatementType.Tenant,
                    ChargedAmount = 350,
                    PaidAmount = 600,
                    HousingBenefitAmount = 400,
                    StartBalance = 1100,
                    FinishBalance = 500
                },
                new Statement
                {
                    Id = new Guid("5a0127f7-a9e5-4a50-aff6-d8f9eccc1fd3"),
                    TargetId = new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"),
                    TargetType = TargetType.Block,
                    StatementPeriodEndDate = new DateTime(2021, 8, 3),
                    RentAccountNumber = "987654321",
                    Address = "15 Macron Court, E8 1ND",
                    StatementType = StatementType.Tenant,
                    ChargedAmount = 350,
                    PaidAmount = 600,
                    HousingBenefitAmount = 400,
                    StartBalance = 1100,
                    FinishBalance = 500
                },

            };
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PaginationToken = string.Empty,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 3)
            };
            var returnedResult = new PagedResult<Statement>(statements);
            var expectedResult = _mapper.Map<PagedResult<StatementResponse>>(returnedResult);
            _mockFinanceGateway.Setup(x => x.GetPagedStatementsAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnedResult);

            var result = await _getStatementListUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), request).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetStatementList_WithEmpty_ReturnsStatementListResponseWithEmptyStatements()
        {
            var expectedStatements = new List<Statement>();
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PaginationToken = string.Empty,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 5)
            };


            var returnedResult = new PagedResult<Statement>(expectedStatements);
            var expectedResult = _mapper.Map<PagedResult<StatementResponse>>(returnedResult);
            _mockFinanceGateway.Setup(x => x.GetPagedStatementsAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnedResult);

            var result = await _getStatementListUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), request).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetStatementList_GatewayReturnsZeroTotal_ReturnsEmptyObject()
        {
            var expectedStatements = new PagedResult<Statement>();
            var request = new GetStatementListRequest
            {
                PageSize = 2,
                PaginationToken = string.Empty,
                StartDate = new DateTime(2021, 8, 3),
                EndDate = new DateTime(2021, 8, 5)
            };
            var expectedResult = _mapper.Map<PagedResult<StatementResponse>>(expectedStatements);
            var returnedResult = new PagedResult<Statement>();
            _mockFinanceGateway.Setup(x => x.GetPagedStatementsAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnedResult);

            var result = await _getStatementListUseCase.ExecuteAsync(new Guid("4f2fb565-84c5-4c8a-9ada-0f03ecd26f45"), request).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
