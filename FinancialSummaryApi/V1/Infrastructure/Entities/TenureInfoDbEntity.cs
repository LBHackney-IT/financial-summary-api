using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Infrastructure.Converters;
using System;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    [DynamoDBTable("tenure_table", LowerCamelCaseProperties = true)]
    public class TenureInfoDbEntity
    {
        [DynamoDBProperty(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "paymentReference")]
        public string PaymentReference { get; set; }

        [DynamoDBProperty(AttributeName = "tenuredAsset", Converter = typeof(DynamoDbObjectConverter<TenureAssetDbEntity>))]
        public TenureAssetDbEntity TenuredAsset { get; set; }
    }
}
