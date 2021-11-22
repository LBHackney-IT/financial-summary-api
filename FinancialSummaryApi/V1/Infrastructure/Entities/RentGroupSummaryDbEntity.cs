using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    /// <summary>
    /// DynamoDB model for the table finance_summary
    /// </summary>
    [DynamoDBTable("FinancialSummaries", LowerCamelCaseProperties = true)]
    public class RentGroupSummaryDbEntity
    {
        [DynamoDBHashKey(AttributeName = "pk")]
        public string Pk { get; set; }
        [DynamoDBRangeKey(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "summary_type", Converter = typeof(DynamoDbEnumConverter<SummaryType>))]
        public SummaryType SummaryType { get; set; }

        [DynamoDBProperty(AttributeName = "submit_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime SubmitDate { get; set; }

        [DynamoDBProperty(AttributeName = "target_description")]
        public string TargetDescription { get; set; }

        [DynamoDBProperty(AttributeName = "target_name")]
        public string TargetName { get; set; }

        [DynamoDBProperty(AttributeName = "total_charged")]
        public decimal TotalCharged { get; set; }

        [DynamoDBProperty(AttributeName = "charged_ytd")]
        public decimal ChargedYTD { get; set; }

        [DynamoDBProperty(AttributeName = "total_paid")]
        public decimal TotalPaid { get; set; }

        [DynamoDBProperty(AttributeName = "paid_ytd")]
        public decimal PaidYTD { get; set; }

        [DynamoDBProperty(AttributeName = "arrears_ytd")]
        public decimal ArrearsYTD { get; set; }

        [DynamoDBProperty(AttributeName = "total_balance")]
        public decimal TotalBalance { get; set; }
    }
}
