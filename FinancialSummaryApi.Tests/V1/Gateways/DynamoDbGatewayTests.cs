using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AutoFixture;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
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

        public DynamoDbGatewayTests()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _amazonDynamoDB = new Mock<IAmazonDynamoDB>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _amazonDynamoDB.Object);
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

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(Guid.NewGuid().ToString()) },
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

            var types = Enum.GetValues(typeof(TargetType));

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(Guid.NewGuid().ToString()) },
                { "target_id", new AttributeValue(targetId.ToString()) },
                { "target_type", new AttributeValue(types.GetValue(rand.Next(0, types.Length - 1)).ToString()) },
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

            return new Dictionary<string, AttributeValue>()
            {
                { "id", new AttributeValue(id.ToString()) },
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
    }
}
