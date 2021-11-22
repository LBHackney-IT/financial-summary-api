using Amazon.DynamoDBv2.DataModel;
using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    [DynamoDBTable("FinancialSummaries", LowerCamelCaseProperties = true)]
    public class StatementDbEntity
    {
        [DynamoDBHashKey(AttributeName = "pk")]
        public string Pk { get; set; }
        [DynamoDBRangeKey(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "target_id")]
        public Guid TargetId { get; set; }

        [DynamoDBProperty(AttributeName = "target_type", Converter = typeof(DynamoDbEnumConverter<TargetType>))]
        public TargetType TargetType { get; set; }

        [DynamoDBProperty(AttributeName = "summary_type", Converter = typeof(DynamoDbEnumConverter<SummaryType>))]
        public SummaryType SummaryType { get; set; }

        [DynamoDBProperty(AttributeName = "statement_period_end_date", Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime StatementPeriodEndDate { get; set; }

        [DynamoDBProperty(AttributeName = "rent_account_number")]
        public string RentAccountNumber { get; set; }

        [DynamoDBProperty(AttributeName = "address")]
        public string Address { get; set; }

        [DynamoDBProperty(AttributeName = "statement_type", Converter = typeof(DynamoDbEnumConverter<StatementType>))]
        public StatementType StatementType { get; set; }

        [DynamoDBProperty(AttributeName = "charged_amount")]
        public decimal ChargedAmount { get; set; }

        [DynamoDBProperty(AttributeName = "paid_amount")]
        public decimal PaidAmount { get; set; }

        [DynamoDBProperty(AttributeName = "housing_benefit_amount")]
        public decimal HousingBenefitAmount { get; set; }

        [DynamoDBProperty(AttributeName = "start_balance")]
        public decimal StartBalance { get; set; }

        [DynamoDBProperty(AttributeName = "finish_balance")]
        public decimal FinishBalance { get; set; }
    }
}
