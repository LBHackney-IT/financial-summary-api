using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.Gateways
{
    public class DynamoDbContextWrapper
    {
        public virtual Task<List<FinanceSummaryDbEntity>> ScanAsync(IDynamoDBContext context, IEnumerable<ScanCondition> conditions, DynamoDBOperationConfig operationConfig = null)
        {
            return context.ScanAsync<FinanceSummaryDbEntity>(conditions, operationConfig).GetRemainingAsync();
        }
        public virtual Task<List<WeeklySummaryDbEntity>> ScanSummaryAsync(IDynamoDBContext context, IEnumerable<ScanCondition> conditions, DynamoDBOperationConfig operationConfig = null)
        {
            return context.ScanAsync<WeeklySummaryDbEntity>(conditions, operationConfig).GetRemainingAsync();
        }
        public virtual Task<WeeklySummaryDbEntity> LoadSummaryAsync(IDynamoDBContext context, Guid id, DynamoDBOperationConfig operationConfig = null)
        {
            return context.LoadAsync<WeeklySummaryDbEntity>(id);
        }
    }
}
