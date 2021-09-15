using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Converters;
using System;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    /// <summary>
    /// DynamoDB model for the table finance_summary
    /// </summary>
    [DynamoDBTable("FinancialSummaries", LowerCamelCaseProperties = true)]
    public class FinanceSummaryDbEntity
    {
        [DynamoDBHashKey(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("target_id_dx")]
        [DynamoDBProperty(AttributeName = "target_id")]
        public Guid TargetId { get; set; }

        [DynamoDBProperty(AttributeName = "target_type", Converter = typeof(DynamoDbEnumConverter<TargetType>))]
        public TargetType TargetType { get; set; }

        [DynamoDBProperty(AttributeName = "submit_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime SubmitDate { get; set; }

        [DynamoDBProperty(AttributeName = "asset_summary", Converter = typeof(DynamoDbObjectConverter<AssetSummaryDbEntity>))]
        public AssetSummaryDbEntity AssetSummaryData { get; set; }

        [DynamoDBProperty(AttributeName = "rent_group_summary", Converter = typeof(DynamoDbObjectConverter<RentGroupSummaryDbEntity>))]
        public RentGroupSummaryDbEntity RentGroupSummaryData { get; set; }
    }
}
