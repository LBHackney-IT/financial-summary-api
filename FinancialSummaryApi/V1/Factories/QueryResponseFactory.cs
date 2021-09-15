using Amazon.DynamoDBv2.Model;
using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialSummaryApi.V1.Factories
{
    public static class QueryResponseFactory
    {
        public static List<AssetSummary> ToAssets(this QueryResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            List<AssetSummary> assets = new List<AssetSummary>();
            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                assets.Add(new AssetSummary
                {
                    Id = Guid.Parse(item["id"].S),
                    TargetId = Guid.Parse(item["target_id"].S),
                    TargetType = (TargetType)Enum.Parse(typeof(TargetType), item["target_type"].S),
                    AssetName = item["asset_summary"].M["assetName"].S,
                    SubmitDate = DateTime.Parse(item["submit_date"].S),
                    TotalDwellingRent = decimal.Parse(item["asset_summary"].M["totalDwellingRent"].N),
                    TotalNonDwellingRent = decimal.Parse(item["asset_summary"].M["totalNonDwellingRent"].N),
                    TotalRentalServiceCharge = decimal.Parse(item["asset_summary"].M["totalRentalServiceCharge"].N),
                    TotalServiceCharges = decimal.Parse(item["asset_summary"].M["totalServiceCharges"].N),
                    TotalIncome = decimal.Parse(item["asset_summary"].M["totalIncome"].N),
                    TotalExpenditure = decimal.Parse(item["asset_summary"].M["totalExpenditure"].N)
                });
            }

            return assets;
        }

        public static WeeklySummary ToWeeklySummary(this QueryResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            List<WeeklySummary> weeklySummaries = new List<WeeklySummary>();
            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                weeklySummaries.Add(new WeeklySummary
                {
                    Id = Guid.Parse(item["id"].S),
                    TargetId = Guid.Parse(item["target_id"].S),
                    PeriodNo = short.Parse(item["period_no"].N),
                    FinancialYear = short.Parse(item["financial_year"].N),
                    FinancialMonth = short.Parse(item["financial_month"].N),
                    WeekStartDate = DateTime.Parse(item["week_start_date"].S),
                    ChargedAmount = decimal.Parse(item["charged_amount"].N),
                    PaidAmount = decimal.Parse(item["paid_amount"].N),
                    BalanceAmount = decimal.Parse(item["balance_amount"].N),
                    HousingBenefitAmount = decimal.Parse(item["housing_benefit_amount"].N)
                });
            }

            // Hanna Holasava
            // weeklySummaries must contain only 1 element
            return weeklySummaries.SingleOrDefault();
        }
    }
}
