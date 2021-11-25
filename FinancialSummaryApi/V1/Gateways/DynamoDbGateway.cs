using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AutoMapper;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FinancialSummaryApi.V1.UseCase.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways
{
    public class DynamoDbGateway : IFinanceSummaryGateway
    {
        private readonly IAmazonDynamoDB _amazonDynamoDb;
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IMapper _mapper;
        private const string PartitionKey = "#financialSummaries";
        public DynamoDbGateway(IDynamoDBContext dynamoDbContext, IAmazonDynamoDB amazonDynamoDb, IMapper mapper)
        {
            _dynamoDbContext = dynamoDbContext;
            _amazonDynamoDb = amazonDynamoDb;
            _mapper = mapper;
        }

        #region Asset Summary

        public async Task AddAsync(AssetSummary assetSummary)
        {
            await _dynamoDbContext.SaveAsync(assetSummary.ToDatabase(PartitionKey)).ConfigureAwait(false);
        }

        public async Task<List<AssetSummary>> GetAllAssetSummaryAsync(DateTime submitDate)
        {
            var (submitDateStart, submitDateEnd) = submitDate.GetDayRange();

            QueryRequest getSummaryRequest = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk",
                FilterExpression = "summary_type = :V_summary_type and submit_date between :V_submit_date_start and :V_submit_date_end",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {   { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_submit_date_start", new AttributeValue { S = submitDateStart.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_submit_date_end", new AttributeValue { S = submitDateEnd.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.AssetSummary.ToString() } },
                }
            };

            var data = await _amazonDynamoDb.QueryAsync(getSummaryRequest).ConfigureAwait(false);

            return data.ToAssetSummary().OrderByDescending(r => r.SubmitDate).ToList();
        }

        public async Task<AssetSummary> GetAssetSummaryByIdAsync(Guid assetId, DateTime submitDate)
        {
            var (submitDateStart, submitDateEnd) = submitDate.GetDayRange();

            QueryRequest getAllAssetSummaryRequest = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk",
                FilterExpression = "target_id = :V_target_id and summary_type = :V_summary_type " +
                                   "and submit_date between :V_submit_date_start and :V_submit_date_end",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {   { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_target_id", new AttributeValue{ S = assetId.ToString() } },
                    { ":V_submit_date_start", new AttributeValue { S = submitDateStart.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_submit_date_end", new AttributeValue { S = submitDateEnd.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.AssetSummary.ToString() } },
                }
            };

            var data = await _amazonDynamoDb.QueryAsync(getAllAssetSummaryRequest).ConfigureAwait(false);

            return data.ToAssetSummary().OrderByDescending(r => r.SubmitDate).FirstOrDefault();
        }

        #endregion

        #region Rent Group Summary

        public async Task AddAsync(RentGroupSummary rentGroupSummary)
        {
            await _dynamoDbContext.SaveAsync(rentGroupSummary.ToDatabase(PartitionKey)).ConfigureAwait(false);
        }

        public async Task<List<RentGroupSummary>> GetAllRentGroupSummaryAsync(DateTime submitDate)
        {
            var (submitDateStart, submitDateEnd) = submitDate.GetDayRange();

            QueryRequest getSummaryRequest = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk",
                FilterExpression = "summary_type = :V_summary_type and submit_date between :V_submit_date_start and :V_submit_date_end",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {   { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_submit_date_start", new AttributeValue { S = submitDateStart.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_submit_date_end", new AttributeValue { S = submitDateEnd.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.RentGroupSummary.ToString() } },
                }
            };

            var data = await _amazonDynamoDb.QueryAsync(getSummaryRequest).ConfigureAwait(false);

            return data.ToRentGroupSummary().OrderByDescending(r => r.SubmitDate).ToList();
        }

        public async Task<RentGroupSummary> GetRentGroupSummaryByNameAsync(string rentGroupName, DateTime submitDate)
        {
            var (submitDateStart, submitDateEnd) = submitDate.GetDayRange();

            QueryRequest getSummaryRequest = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk",
                FilterExpression = "target_name = :V_target_name and summary_type = :V_summary_type " +
                                   "and submit_date between :V_submit_date_start and :V_submit_date_end",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {   { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_submit_date_start", new AttributeValue { S = submitDateStart.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_submit_date_end", new AttributeValue { S = submitDateEnd.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") } },
                    { ":V_target_name", new AttributeValue { S = rentGroupName } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.RentGroupSummary.ToString() } },
                }
            };

            var data = await _amazonDynamoDb.QueryAsync(getSummaryRequest).ConfigureAwait(false);

            return data.ToRentGroupSummary().OrderByDescending(r => r.SubmitDate).FirstOrDefault();
        }

        #endregion

        #region Get Weekly Summary
        public async Task<List<WeeklySummary>> GetAllWeeklySummaryAsync(Guid targetId, DateTime? startDate, DateTime? endDate)
        {
            QueryRequest getSummaryRequest = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk",
                FilterExpression = "target_id = :V_target_id AND summary_type = :V_summary_type ",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_target_id", new AttributeValue { S = targetId.ToString() } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.WeeklySummary.ToString() } },
                }
            };

            if (startDate.HasValue && endDate.HasValue)
            {
                getSummaryRequest.FilterExpression += "and submit_date between :V_submit_date_start and :V_submit_date_end";
                getSummaryRequest.ExpressionAttributeValues.Add(":V_submit_date_start", new AttributeValue { S = startDate.Value.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") });
                getSummaryRequest.ExpressionAttributeValues.Add(":V_submit_date_end", new AttributeValue { S = endDate.Value.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") });
            }

            var data = await _amazonDynamoDb.QueryAsync(getSummaryRequest).ConfigureAwait(false);

            return data.ToWeeklySummary().OrderByDescending(r => r.WeekStartDate).ToList();
        }

        public async Task AddAsync(WeeklySummary weeklySummary)
        {
            await _dynamoDbContext.SaveAsync(weeklySummary.ToDatabase(PartitionKey)).ConfigureAwait(false);
        }

        public async Task<WeeklySummary> GetWeeklySummaryByIdAsync(Guid id)
        {
            QueryRequest getWeeklySummaryById = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk and id = :V_id",
                FilterExpression = "summary_type = :V_summary_type ",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_id", new AttributeValue{ S = id.ToString() } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.WeeklySummary.ToString() } },
                }
            };

            var data = await _amazonDynamoDb.QueryAsync(getWeeklySummaryById).ConfigureAwait(false);

            return data.ToWeeklySummary().OrderByDescending(r => r.WeekStartDate).FirstOrDefault();
        }
        #endregion

        #region Statement

        public async Task<StatementList> GetPagedStatementsAsync(Guid targetId, DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            var request = new QueryRequest
            {
                TableName = "FinancialSummaries",
                KeyConditionExpression = "pk = :V_pk",
                FilterExpression = "target_id = :V_target_id and summary_type = :V_summary_type ",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {   { ":V_pk", new AttributeValue { S = PartitionKey } },
                    { ":V_target_id", new AttributeValue { S = targetId.ToString() } },
                    { ":V_summary_type", new AttributeValue { S = SummaryType.Statement.ToString() } },
                },
                Select = Select.ALL_ATTRIBUTES
            };

            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                request.FilterExpression += " and statement_period_end_date between :V_start_date and :V_end_date";

                request.ExpressionAttributeValues.Add(":V_start_date", new AttributeValue { S = startDate.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") });
                request.ExpressionAttributeValues.Add(":V_end_date", new AttributeValue { S = endDate.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ") });
            }

            var data = await _amazonDynamoDb.QueryAsync(request).ConfigureAwait(false);
            var totalStatementsCount = data.Count;

            var recordsOnPage = CountRecordsOnPage(totalStatementsCount, pageNumber, pageSize);
            var pagedStatements = new List<Statement>(recordsOnPage);

            if (recordsOnPage != 0)
            {
                var statements = _mapper.Map<List<Statement>>(data);
                pagedStatements.AddRange(statements.Skip((pageNumber - 1) * pageSize).Take(pageSize));
            }

            return new StatementList
            {
                Total = totalStatementsCount,
                Statements = pagedStatements
            };
        }

        public async Task<List<Statement>> GetStatementListAsync(Guid targetId, DateTime startDate, DateTime endDate)
        {

            var config = new DynamoDBOperationConfig()
            {
                QueryFilter = new List<ScanCondition>() {
                    new ScanCondition("TargetId", ScanOperator.Equal, targetId),
                    new ScanCondition("StatementPeriodEndDate", ScanOperator.Between,startDate,endDate)
                }
            };
            var data = await _dynamoDbContext.QueryAsync<StatementDbEntity>(PartitionKey, config).GetRemainingAsync().ConfigureAwait(false);
            var result = _mapper.Map<List<Statement>>(data);
            return result.OrderBy(x => x.StatementPeriodEndDate).ToList();
        }
        public async Task AddRangeAsync(List<Statement> statements)
        {
            var statementBatch = _dynamoDbContext.CreateBatchWrite<StatementDbEntity>();
            var statementsDb = _mapper.Map<IEnumerable<StatementDbEntity>>(statements);
            foreach (var item in statementsDb)
            {
                item.Pk = PartitionKey;
            }
            statementBatch.AddPutItems(statementsDb);
            await statementBatch.ExecuteAsync().ConfigureAwait(false);
        }

        #endregion
        private static bool PageCanBeLoaded(int totalRecordsCount, int pageNumber, int pageSize)
            => totalRecordsCount > (pageNumber - 1) * pageSize;

        private static int CountRecordsOnPage(int totalRecordsCount, int pageNumber, int pageSize)
            => PageCanBeLoaded(totalRecordsCount, pageNumber, pageSize) ?
                        Math.Min(pageSize, totalRecordsCount - (pageNumber - 1) * pageSize) : 0;

        public async Task<Statement> GetStatementByIdAsync(Guid id)
        {
            var data = await _dynamoDbContext.LoadAsync<StatementDbEntity>(PartitionKey, id).ConfigureAwait(false);
            return _mapper.Map<Statement>(data);
        }
    }
}
