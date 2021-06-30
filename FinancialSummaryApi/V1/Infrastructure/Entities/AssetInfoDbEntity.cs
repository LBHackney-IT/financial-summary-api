using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Infrastructure.Entities
{
    [DynamoDBTable("asset_information_table", LowerCamelCaseProperties = true)]
    public class AssetInfoDbEntity
    {
        [DynamoDBProperty(AttributeName = "id")]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "assetId")]
        public string AssetId { get; set; }

        [DynamoDBProperty(AttributeName = "assetType")]
        public List<string> AssetType { get; set; }
    }
}
