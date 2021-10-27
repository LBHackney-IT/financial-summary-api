using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AutoFixture;
using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.Infrastructure;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Gateways
{
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDynamoDBContext> _dynamoDb;
        private readonly Mock<IAmazonDynamoDB> _amazonDynamoDB;
        private readonly DynamoDbGateway _gateway;
        private static IMapper _mapper;

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _amazonDynamoDB = new Mock<IAmazonDynamoDB>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                    mc.AddProfile(new MappingProfile()));
                _mapper = mappingConfig.CreateMapper();
            }
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _amazonDynamoDB.Object, _mapper);
        }

        #region Assets
        [Fact]
        public async Task GetByTargetIdReturnsNullIfEntityDoesntExist()
        {
            _amazonDynamoDB.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse());

            var assetSummary = await _gateway.GetAssetSummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"), new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            assetSummary.Should().BeNull();
        }

        [Fact]
        public async Task GetByTargetIdReturnsAssetSummaryIfItExists()
        {
            _amazonDynamoDB.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse()
                {
                    Items = new List<Dictionary<string, AttributeValue>>()
                    { AssetDbResponse.Items[3] }
                });

            var assetSummaryDomain = await _gateway.GetAssetSummaryByIdAsync(Guid.Parse("9f84f01b-fb23-43bf-9bf3-6cb37faa89c7"), new DateTime(2020, 8, 15))
                .ConfigureAwait(false);

            assetSummaryDomain.Should().BeEquivalentTo(AssetDbResponse.ToAssetSummary()[3]);
        }

        [Fact]
        public async Task GetAllAssetsByDateReturnsList()
        {
            _amazonDynamoDB.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse()
                {
                    Items = new List<Dictionary<string, AttributeValue>>()
                    { AssetDbResponse.Items[1], AssetDbResponse.Items[2] }
                });

            var assetSummaries = await _gateway.GetAllAssetSummaryAsync(new DateTime(2020, 9, 9))
                .ConfigureAwait(false);

            assetSummaries.Should().HaveCount(2);

            assetSummaries[0].Should().BeEquivalentTo(AssetDbResponse.ToAssetSummary()[1]);
            assetSummaries[1].Should().BeEquivalentTo(AssetDbResponse.ToAssetSummary()[2]);
        }

        [Fact]
        public async Task AddAssetSummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<AssetSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddAssetSummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((AssetSummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<AssetSummaryDbEntity>(), default), Times.Once);
        }
        #endregion

        #region RentGroups
        [Fact]
        public async Task GetByNameReturnsNullIfEntityDoesntExist()
        {
            _amazonDynamoDB
                .Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse());

            var rentGroupSummary = await _gateway.GetRentGroupSummaryByNameAsync("unknown rent group", new DateTime(2021, 7, 2))
                .ConfigureAwait(false);

            rentGroupSummary.Should().BeNull();
        }

        [Fact]
        public async Task GetByNameReturnsRentGroupSummaryIfItExists()
        {
            _amazonDynamoDB
               .Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new QueryResponse()
               {
                   Items = new List<Dictionary<string, AttributeValue>>()
                    { RentGroupDbResponse.Items[2] }
               });

            var rentGroupSummary = await _gateway.GetRentGroupSummaryByNameAsync("target name 3", new DateTime(2020, 9, 9))
                .ConfigureAwait(false);

            rentGroupSummary.Should().BeEquivalentTo(RentGroupDbResponse.ToRentGroupSummary()[2]);
        }

        [Fact]
        public async Task GetAllRentGroupsByDateReturnsList()
        {
            _amazonDynamoDB
               .Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new QueryResponse()
               {
                   Items = new List<Dictionary<string, AttributeValue>>()
                    { RentGroupDbResponse.Items[1], RentGroupDbResponse.Items[2] }
               });

            var assetSummaries = await _gateway.GetAllRentGroupSummaryAsync(new DateTime(2020, 9, 9))
                .ConfigureAwait(false);

            assetSummaries.Should().HaveCount(2);

            assetSummaries[0].Should().BeEquivalentTo(RentGroupDbResponse.ToRentGroupSummary()[1]);
            assetSummaries[1].Should().BeEquivalentTo(RentGroupDbResponse.ToRentGroupSummary()[2]);
        }

        [Fact]
        public async Task AddRentGroupSummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<RentGroupSummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddRentGroupSummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((RentGroupSummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<RentGroupSummaryDbEntity>(), default), Times.Once);
        }
        #endregion

        #region WeeklySummaries
        [Fact]
        public async Task GetByIdReturnsNullIfEntityDoesntExist()
        {
            _amazonDynamoDB
               .Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new QueryResponse());

            var weeklySummary = await _gateway.GetWeeklySummaryByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"))
                .ConfigureAwait(false);


            weeklySummary.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdReturnsWeeklySummaryIfItExists()
        {
            _amazonDynamoDB
               .Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new QueryResponse()
               {
                   Items = new List<Dictionary<string, AttributeValue>>()
                    { WeeklyDbResponse.Items[3] }
               });

            var weeklySummaryDomain = await _gateway.GetWeeklySummaryByIdAsync(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"))
                .ConfigureAwait(false);

            weeklySummaryDomain.Should().BeEquivalentTo(WeeklyDbResponse.ToWeeklySummary()[3]);
        }

        [Fact]
        public async Task GetAllWeeklySummariesByDateReturnsList()
        {
            _amazonDynamoDB
               .Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new QueryResponse()
               {
                   Items = new List<Dictionary<string, AttributeValue>>()
                    { WeeklyDbResponse.Items[2], WeeklyDbResponse.Items[3] }
               });

            var weeklySummaries = await _gateway.GetAllWeeklySummaryAsync(
                Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"),
                new DateTime(2020, 8, 15),
                new DateTime(2020, 9, 9))
                .ConfigureAwait(false);

            weeklySummaries.Should().HaveCount(2);

            weeklySummaries[0].Should().BeEquivalentTo(WeeklyDbResponse.ToWeeklySummary()[2]);
            weeklySummaries[1].Should().BeEquivalentTo(WeeklyDbResponse.ToWeeklySummary()[3]);
        }

        [Fact]
        public async Task AddWeeklySummaryWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<WeeklySummary>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), default), Times.Once);
        }

        [Fact]
        public async Task AddWeeklySummaryWithInvalidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _gateway.AddAsync((WeeklySummary) null).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<WeeklySummaryDbEntity>(), default), Times.Once);
        }
        #endregion

        #region Statements

        [Fact]
        public async Task GetPagedStatementsAsyncFirstPageReturnsZeroTotal()
        {
            var expectedTotal = 0;
            _amazonDynamoDB.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResponse()
                {
                    Count = expectedTotal,
                    Items = null
                });

            var targetId = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
            var request = new GetStatementListRequest()
            {
                PageNumber = 1,
                PageSize = 2,
                StartDate = new DateTime(2021, 8, 15),
                EndDate = new DateTime(2021, 10, 15)
            };
            var resultStatementsList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PageNumber)
                .ConfigureAwait(false);

            resultStatementsList.Should().NotBeNull();
            resultStatementsList.Total.Should().Be(expectedTotal);
            resultStatementsList.Statements.Should().NotBeNull();
            resultStatementsList.Statements.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPagedStatementsFirstPageReturnsList()
        {
            var targetId = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
            int expectedTotal = 3;
            _amazonDynamoDB.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new QueryResponse()
                   {
                       Count = expectedTotal,
                       Items = new List<Dictionary<string, AttributeValue>>()
                     { StatementDbResponse.Items[0], StatementDbResponse.Items[2], StatementDbResponse.Items[5] }
                   });

            var request = new GetStatementListRequest()
            {
                PageNumber = 1,
                PageSize = 2,
                StartDate = new DateTime(2021, 8, 15),
                EndDate = new DateTime(2021, 10, 15)
            };
            var statementsDbResponse = _mapper.Map<List<Statement>>(StatementDbResponse);
            var expectedStatementFirst = statementsDbResponse[0];
            var expectedStatementSecond = statementsDbResponse[2];

            var statementList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PageNumber)
                .ConfigureAwait(false);

            statementList.Should().NotBeNull();
            statementList.Total.Should().Be(expectedTotal);
            statementList.Statements.Should().NotBeNull();
            statementList.Statements.Should().HaveCount(2);

            statementList.Statements[0].Should().BeEquivalentTo(expectedStatementFirst);
            statementList.Statements[1].Should().BeEquivalentTo(expectedStatementSecond);
        }

        [Fact]
        public async Task GetPagedStatementsSecondPageReturnsList()
        {
            int expectedTotal = 3;
            _amazonDynamoDB.Setup(_ => _.QueryAsync(It.IsAny<QueryRequest>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(new QueryResponse()
                  {
                      Count = expectedTotal,
                      Items = new List<Dictionary<string, AttributeValue>>()
                    { StatementDbResponse.Items[0], StatementDbResponse.Items[2], StatementDbResponse.Items[5] }
                  });

            var targetId = new Guid("fdd9c513-50b0-4fde-ae75-176f8208c4cd");
            var request = new GetStatementListRequest()
            {
                PageNumber = 2,
                PageSize = 2,
                StartDate = new DateTime(2021, 8, 15),
                EndDate = new DateTime(2021, 10, 15)
            };
            var statementsDbResponse = _mapper.Map<List<Statement>>(StatementDbResponse);
            var expectedStatement = statementsDbResponse[5];

            var statementList = await _gateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PageNumber)
                .ConfigureAwait(false);

            statementList.Should().NotBeNull();
            statementList.Total.Should().Be(expectedTotal);
            statementList.Statements.Should().NotBeNull();
            statementList.Statements.Should().HaveCount(1);

            statementList.Statements[0].Should().BeEquivalentTo(expectedStatement);
        }

        #endregion

        private QueryResponse _rentGroupDbResponse;
        private QueryResponse RentGroupDbResponse
        {
            get
            {
                if (_rentGroupDbResponse == null)
                {
                    _rentGroupDbResponse = new QueryResponse()
                    {
                        Items = new List<Dictionary<string, AttributeValue>>()
                        {
                            CreateRentGroupDbRecord("target name 1", new DateTime(2021, 10, 15)),
                            CreateRentGroupDbRecord("target name 2", new DateTime(2020, 9, 9)),
                            CreateRentGroupDbRecord("target name 3", new DateTime(2020, 9, 9)),
                            CreateRentGroupDbRecord("target name 4", new DateTime(2020, 8, 15))
                        }
                    };
                }

                return _rentGroupDbResponse;
            }
        }

        private static Dictionary<string, AttributeValue> CreateRentGroupDbRecord(string rentGroupName, DateTime submitDate)
        {
            Random rand = new Random();
            var summaryTypes = Enum.GetValues(typeof(SummaryType));

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(Guid.NewGuid().ToString()) },
                { "summary_type", new AttributeValue(summaryTypes.GetValue(rand.Next(0, summaryTypes.Length - 1)).ToString()) },
                { "arrears_ytd", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "charged_ytd", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "paid_ytd", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "target_name", new AttributeValue(rentGroupName) },
                { "target_description", new AttributeValue("target description 1") },
                { "total_balance", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "total_charged", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "total_paid", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "submit_date", new AttributeValue(submitDate.ToString()) },
            };
        }

        private QueryResponse _assetDbResponse;
        private QueryResponse AssetDbResponse
        {
            get
            {
                if (_assetDbResponse == null)
                {
                    _assetDbResponse = new QueryResponse()
                    {
                        Items = new List<Dictionary<string, AttributeValue>>()
                        {
                            CreateAssetDbRecord(Guid.Parse("fdd9c513-50b0-4fde-ae75-176f8208c4cd"), new DateTime(2021, 10, 15)),
                            CreateAssetDbRecord(Guid.Parse("333244c1-d125-4c04-a306-6f2e337961a2"), new DateTime(2020, 9, 9)),
                            CreateAssetDbRecord(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"), new DateTime(2020, 9, 9)),
                            CreateAssetDbRecord(Guid.Parse("9f84f01b-fb23-43bf-9bf3-6cb37faa89c7"), new DateTime(2020, 8, 15))
                        }
                    };
                }

                return _assetDbResponse;
            }
        }

        private static Dictionary<string, AttributeValue> CreateAssetDbRecord(Guid targetId, DateTime submitDate)
        {
            Random rand = new Random();

            var targetTypes = Enum.GetValues(typeof(TargetType));
            var summaryTypes = Enum.GetValues(typeof(SummaryType));

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(Guid.NewGuid().ToString()) },
                { "target_id", new AttributeValue(targetId.ToString()) },
                { "summary_type", new AttributeValue(summaryTypes.GetValue(rand.Next(0, summaryTypes.Length - 1)).ToString()) },
                { "target_type", new AttributeValue(targetTypes.GetValue(rand.Next(0, targetTypes.Length - 1)).ToString()) },
                { "target_name", new AttributeValue("Some target name") },
                { "submit_date", new AttributeValue(submitDate.ToString()) },
                { "total_dwelling_rent", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() } },
                { "total_non_dwelling_rent", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() } },
                { "total_rental_service_charge", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() } },
                { "total_service_charges", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() } },
                { "total_income", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() }},
                { "total_expenditure", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() } },
            };
        }

        private QueryResponse _weeklyDbResponse;
        private QueryResponse WeeklyDbResponse
        {
            get
            {
                if (_weeklyDbResponse == null)
                {
                    _weeklyDbResponse = new QueryResponse()
                    {
                        Items = new List<Dictionary<string, AttributeValue>>()
                        {
                            CreateWeeklyDbRecord(Guid.Parse("fdd9c513-50b0-4fde-ae75-176f8208c4cd"), new DateTime(2021, 10, 15)),
                            CreateWeeklyDbRecord(Guid.Parse("333244c1-d125-4c04-a306-6f2e337961a2"), new DateTime(2020, 9, 9)),
                            CreateWeeklyDbRecord(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"), new DateTime(2020, 9, 9)),
                            CreateWeeklyDbRecord(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"), new DateTime(2020, 8, 15))
                        }
                    };
                }

                return _weeklyDbResponse;
            }
        }

        private static Dictionary<string, AttributeValue> CreateWeeklyDbRecord(Guid id, DateTime weeklyStartDate)
        {
            Random rand = new Random();
            var summaryTypes = Enum.GetValues(typeof(SummaryType));

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(id.ToString()) },
                { "summary_type", new AttributeValue(summaryTypes.GetValue(rand.Next(0, summaryTypes.Length - 1)).ToString()) },
                { "target_id", new AttributeValue(Guid.NewGuid().ToString()) },
                { "period_no", new AttributeValue(){ N = rand.Next(0, 10).ToString() } },
                { "financial_year", new AttributeValue(){ N = rand.Next(2018, 2021).ToString() } },
                { "financial_month", new AttributeValue() { N = rand.Next(1, 12).ToString() } },
                { "week_start_date", new AttributeValue(weeklyStartDate.ToString()) },
                { "charged_amount", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "paid_amount", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "balance_amount", new AttributeValue(){ N = (rand.NextDouble() * 50).ToString() } },
                { "housing_benefit_amount", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "submit_date", new AttributeValue(DateTime.Now.ToString()) },
            };
        }

        private QueryResponse _statementDbResponse;
        private QueryResponse StatementDbResponse
        {
            get
            {
                if (_statementDbResponse == null)
                {
                    _statementDbResponse = new QueryResponse()
                    {
                        Items = new List<Dictionary<string, AttributeValue>>()
                        {
                            CreateStatementDbRecord(Guid.Parse("fdd9c513-50b0-4fde-ae75-176f8208c4cd"), new DateTime(2021, 8, 15)),
                            CreateStatementDbRecord(Guid.Parse("333244c1-d125-4c04-a306-6f2e337961a2"), new DateTime(2020, 9, 9)),
                            CreateStatementDbRecord(Guid.Parse("fdd9c513-50b0-4fde-ae75-176f8208c4cd"), new DateTime(2021, 10, 15)),
                            CreateStatementDbRecord(Guid.Parse("4fc2872e-5131-4399-8959-c4a17b611f9c"), new DateTime(2020, 9, 9)),
                            CreateStatementDbRecord(Guid.Parse("9f84f01b-fb23-43bf-9bf3-6cb37faa89c7"), new DateTime(2020, 8, 15)),
                            CreateStatementDbRecord(Guid.Parse("fdd9c513-50b0-4fde-ae75-176f8208c4cd"), new DateTime(2021, 9, 15)),
                        }
                    };
                }

                return _statementDbResponse;
            }
        }

        private static Dictionary<string, AttributeValue> CreateStatementDbRecord(Guid targetId, DateTime statementPeriodEndDate)
        {
            Random rand = new Random();

            var targetTypes = Enum.GetValues(typeof(TargetType));
            var summaryTypes = Enum.GetValues(typeof(SummaryType));
            var statementTypes = Enum.GetValues(typeof(StatementType));

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(Guid.NewGuid().ToString()) },
                { "target_id", new AttributeValue(targetId.ToString()) },
                { "target_type", new AttributeValue(targetTypes.GetValue(rand.Next(0, targetTypes.Length - 1)).ToString()) },
                { "summary_type", new AttributeValue(summaryTypes.GetValue(rand.Next(0, summaryTypes.Length - 1)).ToString()) },
                { "statement_period_end_date", new AttributeValue(statementPeriodEndDate.ToString()) },
                { "rent_account_number", new AttributeValue("Some account number") },
                { "address", new AttributeValue("Some address") },
                { "statement_type", new AttributeValue(statementTypes.GetValue(rand.Next(0, statementTypes.Length - 1)).ToString()) },
                { "charged_amount", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "paid_amount", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "housing_benefit_amount", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "start_balance", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
                { "finish_balance", new AttributeValue() { N = (rand.NextDouble() * 50).ToString() } },
            };
        }
    }
}
