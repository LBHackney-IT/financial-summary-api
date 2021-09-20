using Amazon.DynamoDBv2.DataModel;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    /// <summary>
    /// DynamoDB model for the table finance_summary
    /// </summary>
    public class RentGroupSummaryDbEntity
    {
        [DynamoDBProperty(AttributeName = "target_description")]
        public string TargetDescription { get; set; }

        [DynamoDBProperty(AttributeName = "rent_group_name")]
        [DynamoDBGlobalSecondaryIndexHashKey("rent_group_name_dx")]
        public string RentGroupName { get; set; }

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
