using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FinancialSummaryApi.V1.Factories
{
    public static class ResponseFactory
    {
        public static RentGroupSummaryResponse ToResponse(this RentGroupSummary domain)
        {
            return domain == null ? null : new RentGroupSummaryResponse()
            {
                Id = domain.Id,
                SubmitDate = domain.SubmitDate,
                TotalArrears = domain.TotalArrears,
                ChargedYTD = domain.ChargedYTD,
                PaidYTD = domain.PaidYTD,
                TargetDescription = domain.TargetDescription,
                TotalBalance = domain.TotalBalance,
                TotalCharged = domain.TotalCharged,
                TotalPaid = domain.TotalPaid,
                RentGroupName = domain.RentGroupName,
            };
        }

        public static List<RentGroupSummaryResponse> ToResponse(this IEnumerable<RentGroupSummary> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }

        public static AssetSummaryResponse ToResponse(this AssetSummary domain)
        {
            return domain == null ? null : new AssetSummaryResponse
            {
                Id = domain.Id,
                TargetId = domain.TargetId,
                ValuesType = domain.ValuesType,
                TargetType = domain.TargetType,
                SubmitDate = domain.SubmitDate,
                TotalDwellingRent = domain.TotalDwellingRent,
                TotalNonDwellingRent = domain.TotalNonDwellingRent,
                TotalRentalServiceCharge = domain.TotalRentalServiceCharge,
                TotalServiceCharges = domain.TotalServiceCharges,
                TotalIncome = domain.TotalIncome,
                TotalExpenditure = domain.TotalExpenditure,
                AssetName = domain.AssetName,
                SummaryYear = domain.SummaryYear,
                TotalDwellings = domain.TotalDwellings,
                TotalFreeholders = domain.TotalFreeholders,
                TotalLeaseholders = domain.TotalLeaseholders
            };
        }

        public static List<AssetSummaryResponse> ToResponse(this IEnumerable<AssetSummary> domainList)
        {
            return domainList?.Select(domain => domain.ToResponse())?.ToList();
        }

        public static AssetSummaryViewResponse ToViewResponse(this AssetSummary domain)
        {
            return new AssetSummaryViewResponse
            {
                Id = domain.Id,
                TargetId = domain.TargetId,
                SummaryYear = domain.SummaryYear,
                Type = domain.ValuesType,
                Ownership = new OwnershipResponse
                {
                    TotalLeaseholders = domain.TotalLeaseholders,
                    TotalFreeholders = domain.TotalFreeholders,
                    TotalDwellings = domain.TotalDwellings
                },
                TotalServiceCharges = domain.TotalServiceCharges
            };
        }

        public static List<AssetSummaryViewResponse> ToViewResponse(this IList<AssetSummary> domainList)
        {
            return domainList.Select(domain => domain?.ToViewResponse()).ToList();
        }
    }
}