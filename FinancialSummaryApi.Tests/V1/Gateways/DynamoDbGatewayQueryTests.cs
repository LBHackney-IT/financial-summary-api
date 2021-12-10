using AutoFixture;
using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Hackney.Core.DynamoDb;
using Hackney.Core.Testing.DynamoDb;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    [Collection("AppTest collection")]
    public class DynamoDbGatewayQueryTests : IClassFixture<MockWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly DynamoDbGateway _gateway;
        private static Mock<IMapper> _mapper;
        private readonly IDynamoDbFixture _dbFixture;
        private readonly MockWebApplicationFactory<Startup> _factory;
        private readonly Guid _rentGroupTargetId = new Guid("51259000-0dfd-4c74-8e25-45a9c7f2fc90");

        private readonly List<Action> _cleanup = new List<Action>();
        public DynamoDbGatewayQueryTests(MockWebApplicationFactory<Startup> appFactory)
        {
            _factory = appFactory;
            _dbFixture = _factory.DynamoDbFixture;
            _mapper = new Mock<IMapper>();
            _gateway = new DynamoDbGateway(_dbFixture.DynamoDbContext, _mapper.Object);


        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                foreach (var action in _cleanup)
                    action();

                _disposed = true;
            }
        }

        #region Assets
        [Fact]
        public async Task GetByTargetIdReturnsNullIfEntityDoesntExist()
        {

            var assetSummary = await _gateway.GetAssetSummaryByIdAsync(new Guid("83dd3d9e-3e63-420d-b22b-2313e2169ae9"), new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummary.Should().BeNull();
        }




        #endregion

        #region RentGroups
        [Fact]
        public async Task GetByNameReturnsNullIfEntityDoesntExist()
        {

            var rentGroupSummary = await _gateway.GetRentGroupSummaryByNameAsync("unknown rent group", new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            rentGroupSummary.Should().BeNull();
        }




        #endregion

        #region Statements

        [Fact]
        public async Task GetPagedStatementsAsyncFirstPageReturnsZeroTotal()
        {
            var expectedTotal = 0;
            var targetId = new Guid("fd9098f4-c4eb-40ce-96c1-b19e14dd072e");
            var request = new GetStatementListRequest()
            {
                PaginationToken = string.Empty,
                PageSize = 2,
                StartDate = new DateTime(2021, 8, 15),
                EndDate = new DateTime(2021, 10, 15)
            };
            var resultStatementsList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PaginationToken).ConfigureAwait(false);
            resultStatementsList.Should().NotBeNull();
            resultStatementsList.PaginationDetails.HasNext.Should().BeFalse();
            resultStatementsList.PaginationDetails.NextToken.Should().BeNull();
            resultStatementsList.Results.Should().HaveCount(expectedTotal);
        }

        [Fact]
        public async Task GetPagedStatementsFirstPageReturnsList()
        {
            var targetId = new Guid("d81ae03a-6aea-40ae-8137-343942f30172");
            int expectedTotal = 3;
            var expected = InsertStatement(targetId, 3);
            var request = new GetStatementListRequest()
            {
                PageSize = expectedTotal,
                StartDate = expected.FirstOrDefault().StatementPeriodEndDate.AddDays(-1),
                EndDate = expected.LastOrDefault().StatementPeriodEndDate.AddDays(1)
            };
            var statementsDbResponse = new PagedResult<Statement>(expected.ToDomain());

            var statementList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PaginationToken)
                .ConfigureAwait(false);


            foreach (var item in expected)
            {
                _cleanup.Add(async () =>
             await _dbFixture.DynamoDbContext.DeleteAsync<StatementDbEntity>(item.TargetId, item.Id, default).ConfigureAwait(false));
            }
            statementList.Should().NotBeNull();
            statementList.PaginationDetails.HasNext.Should().BeFalse();
            statementList.PaginationDetails.NextToken.Should().BeNull();
            statementList.Results.Should().HaveCount(expectedTotal);
        }

        [Fact]
        public async Task GetPagedStatementsSecondPageReturnsList()
        {

            int expectedTotal = 3;

            var targetId = new Guid("e1f4f42b-e69b-4ae7-b8d1-2a5a3d4bb18b");
            var expected = InsertStatement(targetId, 5);
            var request = new GetStatementListRequest()
            {
                PageSize = expectedTotal,
                StartDate = expected.FirstOrDefault().StatementPeriodEndDate.AddDays(-1),
                EndDate = expected.LastOrDefault().StatementPeriodEndDate.AddDays(1)
            };
            var statementsDbResponse = new PagedResult<Statement>(expected.ToDomain());

            var statementList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PaginationToken)
                .ConfigureAwait(false);

            statementList.Should().NotBeNull();
            statementList.PaginationDetails.HasNext.Should().BeTrue();
            statementList.PaginationDetails.NextToken.Should().NotBeNull();
            statementList.Results.Should().NotBeNull();
            statementList.Results.Should().HaveCount(expectedTotal);

            request.PaginationToken = statementList.PaginationDetails.NextToken;
            statementList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PaginationToken)
                .ConfigureAwait(false);

            foreach (var item in expected)
            {
                _cleanup.Add(async () =>
             await _dbFixture.DynamoDbContext.DeleteAsync<StatementDbEntity>(item.TargetId, item.Id, default).ConfigureAwait(false));
            }
            statementList.Should().NotBeNull();
            statementList.PaginationDetails.HasNext.Should().BeFalse();
            statementList.PaginationDetails.NextToken.Should().BeNull();
        }

        #endregion

        private List<AssetSummaryDbEntity> InsertAsset(Guid targetId, DateTime submitDate, int count)
        {
            var assetSummaries = new List<AssetSummaryDbEntity>();

            var random = new Random();
            assetSummaries.AddRange(_fixture.Build<AssetSummaryDbEntity>()
                                   .With(x => x.SubmitDate, submitDate)
                                   .With(x => x.TargetType, TargetType.Block)
                                    .With(x => x.SummaryType, SummaryType.AssetSummary)
                                   .With(x => x.TargetId, targetId)
                                   .CreateMany(count));

            foreach (var item in assetSummaries)
            {
                _dbFixture.DynamoDbContext.SaveAsync(item).GetAwaiter().GetResult();
            }
            var assetSummariesResult = _dbFixture.DynamoDbContext
                .QueryAsync<AssetSummaryDbEntity>(assetSummaries[0].TargetId, null)
                .GetRemainingAsync().GetAwaiter().GetResult().OrderByDescending(x => x.SubmitDate)
                                 .ToList(); ;
            return assetSummariesResult;
        }

        private List<RentGroupSummaryDbEntity> InsertRentGroup(string name, DateTime submitDate, int count)
        {
            var rentGroupSummaries = new List<RentGroupSummaryDbEntity>();

            var random = new Random();
            rentGroupSummaries.AddRange(_fixture.Build<RentGroupSummaryDbEntity>()
                                   .With(x => x.TargetName, name)
                                   .With(x => x.SubmitDate, submitDate)
                                    .With(x => x.SummaryType, SummaryType.RentGroupSummary)
                                   .With(x => x.TargetId, _rentGroupTargetId)
                                   .CreateMany(count));

            foreach (var item in rentGroupSummaries)
            {
                _dbFixture.DynamoDbContext.SaveAsync(item).GetAwaiter().GetResult();
            }
            var rentGroupSummariesResult = _dbFixture.DynamoDbContext
                .QueryAsync<RentGroupSummaryDbEntity>(rentGroupSummaries[0].TargetId, null)
                .GetRemainingAsync().GetAwaiter().GetResult().OrderByDescending(x => x.SubmitDate)
                                 .ToList(); ;
            return rentGroupSummariesResult;
        }

        private List<StatementDbEntity> InsertStatement(Guid targetId, int count)
        {
            var statementDbEntities = new List<StatementDbEntity>();
            var random = new Random();
            Func<DateTime> funcDT = () => DateTime.UtcNow.AddDays(-2 - random.Next(100));
            statementDbEntities.AddRange(_fixture.Build<StatementDbEntity>()
                                   .With(x => x.StatementPeriodEndDate, funcDT)
                                    .With(x => x.SummaryType, SummaryType.Statement)
                                   .With(x => x.TargetId, targetId)
                                   .CreateMany(count));

            foreach (var item in statementDbEntities)
            {
                _dbFixture.DynamoDbContext.SaveAsync(item).GetAwaiter().GetResult();
            }
            var statement = _dbFixture.DynamoDbContext
                .QueryAsync<StatementDbEntity>(statementDbEntities[0].TargetId, null)
                .GetRemainingAsync().GetAwaiter().GetResult().OrderBy(x => x.StatementPeriodEndDate)
                                 .ToList();
            return statement;
        }

    }
}
