using Amazon.DynamoDBv2.DataModel;
using System;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    [DynamoDBTable("TransactionSummaries", LowerCamelCaseProperties = true)]
    public class WeeklySummaryDbEntity
    {
        [DynamoDBHashKey]
        [DynamoDBProperty(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "target_id")]
        [DynamoDBGlobalSecondaryIndexHashKey("target_id_dx")]
        public Guid TargetId { get; set; }

        [DynamoDBProperty(AttributeName = "period_no")]
        public short PeriodNo { get; set; }

        [DynamoDBProperty(AttributeName = "financial_year")]
        public short FinancialYear { get; set; }

        [DynamoDBProperty(AttributeName = "financial_month")]
        public short FinancialMonth { get; set; }

        [DynamoDBProperty(AttributeName = "week_start_date", Converter = (typeof(DynamoDbDateTimeConverter)))]
        public DateTime WeekStartDate { get; set; }

        [DynamoDBProperty(AttributeName = "paid_amount")]
        public decimal PaidAmount { get; set; }

        [DynamoDBProperty(AttributeName = "charged_amount")]
        public decimal ChargedAmount { get; set; }

        [DynamoDBProperty(AttributeName = "balance_amount")]
        public decimal BalanceAmount { get; set; }

        [DynamoDBProperty(AttributeName = "housing_benefit_amount")]
        public decimal HousingBenefitAmount { get; set; }
    }
}
