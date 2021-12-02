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

            var assetSummary = await _gateway.GetAssetSummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummary.Should().BeNull();
        }

        [Fact]
        public async Task GetByTargetIdReturnsAssetSummaryIfItExists()
        {

            var expectedResponse = InsertAsset(Guid.Parse("9f84f01b-fb23-43bf-9bf3-6cb37faa89c7"), new DateTime(2020, 8, 15), 1);
            var domain = expectedResponse[0].ToDomain();
            var assetSummaryDomain = await _gateway.GetAssetSummaryByIdAsync(domain.TargetId, domain.SubmitDate)
                .ConfigureAwait(false);
            foreach (var item in expectedResponse)
            {
                _cleanup.Add(async () =>
               await _dbFixture.DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(item.TargetId, item.Id, default).ConfigureAwait(false));
            }
            assetSummaryDomain.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public async Task GetAllAssetsByDateReturnsList()
        {

            var expectedResponse = InsertAsset(Guid.Parse("36d5bef5-c2fb-42b8-9343-cdd1dcc5e8c8"), new DateTime(2020, 9, 9), 2);
            var query = expectedResponse.FirstOrDefault();
            var assetSummaries = await _gateway.GetAllAssetSummaryAsync(query.TargetId, query.SubmitDate)
                .ConfigureAwait(false);
            foreach (var item in expectedResponse)
            {
                _cleanup.Add(async () =>
                await _dbFixture.DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(item.TargetId, item.Id, default).ConfigureAwait(false));
            }
            assetSummaries.Should().HaveCount(expectedResponse.Count);
            assetSummaries[0].TargetId.Should().Be(expectedResponse[0].TargetId);
            assetSummaries[1].TargetType.Should().BeEquivalentTo(expectedResponse[1].TargetType);
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

        [Fact]
        public async Task GetByNameReturnsRentGroupSummaryIfItExists()
        {
            var expected = InsertRentGroup("target name 3", new DateTime(2020, 9, 9), 1).FirstOrDefault();
            var rentGroupSummary = await _gateway.GetRentGroupSummaryByNameAsync(expected.TargetName, expected.SubmitDate)
                .ConfigureAwait(false);
            _cleanup.Add(async () =>
                await _dbFixture.DynamoDbContext.DeleteAsync<RentGroupSummaryDbEntity>(_rentGroupTargetId, expected.Id, default).ConfigureAwait(false));
            var expectedResponse = expected.ToDomain();
            rentGroupSummary.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetAllRentGroupsByDateReturnsList()
        {
            var expected = InsertRentGroup("target name 1", new DateTime(2020, 9, 9), 2);
            var rentGroupSummaries = await _gateway.GetAllRentGroupSummaryAsync(expected[0].SubmitDate)
                .ConfigureAwait(false);
            foreach (var item in expected)
            {
                _cleanup.Add(async () =>
                await _dbFixture.DynamoDbContext.DeleteAsync<RentGroupSummaryDbEntity>(_rentGroupTargetId, item.Id, default).ConfigureAwait(false));
            }
            rentGroupSummaries.Should().HaveCount(2);
            var expectedResponse = expected.ToDomain();
            rentGroupSummaries.Should().BeEquivalentTo(expectedResponse);
        }


        #endregion

        #region WeeklySummaries
        [Fact]
        public async Task GetByIdReturnsNullIfEntityDoesntExist()
        {

            var weeklySummary = await _gateway.GetWeeklySummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"))
                .ConfigureAwait(false);


            weeklySummary.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdReturnsWeeklySummaryIfItExists()
        {

            var expected = InsertWeeklySummary(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"), DateTime.Today, 1).FirstOrDefault();
            var weeklySummaryResponse = await _gateway.GetWeeklySummaryByIdAsync(expected.TargetId, expected.Id)
                .ConfigureAwait(false);
            _cleanup.Add(async () =>
              await _dbFixture.DynamoDbContext.DeleteAsync<WeeklySummaryDbEntity>(weeklySummaryResponse.TargetId, weeklySummaryResponse.Id, default).ConfigureAwait(false));
            var domain = expected.ToDomain();
            weeklySummaryResponse.Should().BeEquivalentTo(domain);
        }

        [Fact]
        public async Task GetAllWeeklySummariesByDateReturnsList()
        {
            var expected = InsertWeeklySummary(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"), new DateTime(2020, 8, 15).ToUniversalTime(), 2);

            var weeklySummaries = await _gateway.GetAllWeeklySummaryAsync(expected[0].TargetId, expected[0].SubmitDate.AddDays(-1), expected[0].SubmitDate.AddDays(1))
                .ConfigureAwait(false);
            foreach (var item in weeklySummaries)
            {
                _cleanup.Add(async () =>
             await _dbFixture.DynamoDbContext.DeleteAsync<AssetSummaryDbEntity>(item.TargetId, item.Id, default).ConfigureAwait(false));
            }
            weeklySummaries.Should().HaveCount(2);

            weeklySummaries.Should().BeEquivalentTo(expected.ToDomain());
        }


        #endregion

        #region Statements

        [Fact]
        public async Task GetPagedStatementsAsyncFirstPageReturnsZeroTotal()
        {
            var expectedTotal = 0;
            var targetId = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
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
            var targetId = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
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

            var targetId = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
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
        private List<WeeklySummaryDbEntity> InsertWeeklySummary(Guid targetId, DateTime submitDate, int count)
        {
            var weeklySummaries = new List<WeeklySummaryDbEntity>();
            weeklySummaries.AddRange(_fixture.Build<WeeklySummaryDbEntity>()
                                   .With(x => x.SubmitDate, submitDate)
                                    .With(x => x.SummaryType, SummaryType.WeeklySummary)
                                   .With(x => x.TargetId, targetId)
                                   .CreateMany(count));

            foreach (var item in weeklySummaries)
            {
                _dbFixture.DynamoDbContext.SaveAsync(item).GetAwaiter().GetResult();
            }
            var weeklySummaryDbEntities = _dbFixture.DynamoDbContext
                .QueryAsync<WeeklySummaryDbEntity>(weeklySummaries[0].TargetId, null)
                .GetRemainingAsync().GetAwaiter().GetResult().OrderByDescending(x => x.SubmitDate)
                                 .ToList();

            return weeklySummaryDbEntities;
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
