using Amazon.DynamoDBv2.Model;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FinancialSummaryApi.V1.Factories
{
    public static class QueryResponseFactory
    {
        public static List<AssetSummary> ToAssetSummary(this QueryResponse response)
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
                    TargetType = (TargetType) Enum.Parse(typeof(TargetType), item["target_type"].S),
                    AssetName = item["target_name"].S,
                    SubmitDate = DateTime.Parse(item["submit_date"].S),
                    TotalDwellingRent = decimal.Parse(item["total_dwelling_rent"].N, CultureInfo.InvariantCulture),
                    TotalNonDwellingRent = decimal.Parse(item["total_non_dwelling_rent"].N, CultureInfo.InvariantCulture),
                    TotalRentalServiceCharge = decimal.Parse(item["total_rental_service_charge"].N, CultureInfo.InvariantCulture),
                    TotalServiceCharges = decimal.Parse(item["total_service_charges"].N, CultureInfo.InvariantCulture),
                    TotalIncome = decimal.Parse(item["total_income"].N, CultureInfo.InvariantCulture),
                    TotalExpenditure = decimal.Parse(item["total_expenditure"].N, CultureInfo.InvariantCulture),
                });
            }

            return assets;
        }

        public static List<WeeklySummary> ToWeeklySummary(this QueryResponse response)
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
                    ChargedAmount = decimal.Parse(item["charged_amount"].N, CultureInfo.InvariantCulture),
                    PaidAmount = decimal.Parse(item["paid_amount"].N, CultureInfo.InvariantCulture),
                    BalanceAmount = decimal.Parse(item["balance_amount"].N, CultureInfo.InvariantCulture),
                    HousingBenefitAmount = decimal.Parse(item["housing_benefit_amount"].N, CultureInfo.InvariantCulture),
                    SubmitDate = DateTime.Parse(item["submit_date"].S),
                });
            }

            return weeklySummaries;
        }

        public static List<RentGroupSummary> ToRentGroupSummary(this QueryResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            List<RentGroupSummary> summaries = new List<RentGroupSummary>();
            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                // ToDo:
                // Add error handling for all parsing statements
                summaries.Add(new RentGroupSummary
                {
                    Id = Guid.Parse(item["id"].S),
                    ArrearsYTD = decimal.Parse(item["arrears_ytd"].N, CultureInfo.InvariantCulture),
                    ChargedYTD = decimal.Parse(item["charged_ytd"].N, CultureInfo.InvariantCulture),
                    PaidYTD = decimal.Parse(item["paid_ytd"].N, CultureInfo.InvariantCulture),
                    RentGroupName = item["target_name"].S,
                    TargetDescription = item["target_description"].S,
                    TotalBalance = decimal.Parse(item["total_balance"].N, CultureInfo.InvariantCulture),
                    TotalCharged = decimal.Parse(item["total_charged"].N, CultureInfo.InvariantCulture),
                    TotalPaid = decimal.Parse(item["total_paid"].N, CultureInfo.InvariantCulture),
                    SubmitDate = DateTime.Parse(item["submit_date"].S),
                });
            }

            return summaries;
        }

        public static List<Statement> ToStatement(this QueryResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            List<Statement> statements = new List<Statement>();

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                statements.Add(new Statement
                {
                    Id = Guid.Parse(item["id"].S),
                    TargetId = Guid.Parse(item["target_id"].S),
                    TargetType = (TargetType) Enum.Parse(typeof(TargetType), item["target_type"].S),
                    StatementPeriodEndDate = DateTime.Parse(item["statement_period_end_date"].S),
                    RentAccountNumber = item["rent_account_number"].S,
                    Address = item["address"].S,
                    StatementType = (StatementType) Enum.Parse(typeof(StatementType), item["statement_type"].S),
                    ChargedAmount = decimal.Parse(item["charged_amount"].N, CultureInfo.InvariantCulture),
                    PaidAmount = decimal.Parse(item["paid_amount"].N, CultureInfo.InvariantCulture),
                    HousingBenefitAmount = decimal.Parse(item["housing_benefit_amount"].N, CultureInfo.InvariantCulture),
                    StartBalance = decimal.Parse(item["start_balance"].N, CultureInfo.InvariantCulture),
                    FinishBalance = decimal.Parse(item["finish_balance"].N, CultureInfo.InvariantCulture),
                });
            }

            return statements;
        }
    }
}
