using Amazon.DynamoDBv2.DataModel;
using System;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    public class TenureAssetDbEntity
    {
        [DynamoDBProperty(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "type")]
        public string Type { get; set; }

        [DynamoDBProperty(AttributeName = "fullAddress")]
        public string FullAddress { get; set; }
    }
}
