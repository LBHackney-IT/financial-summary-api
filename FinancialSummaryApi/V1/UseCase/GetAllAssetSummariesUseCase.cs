using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAllAssetSummariesUseCase : IGetAllAssetSummariesUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public GetAllAssetSummariesUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        //[LogCall]
        public async Task<List<AssetSummaryViewResponse>> ExecuteAsync(Guid assetId)
        {
            var assetSummaries =
                (await _financeSummaryGateway.GetAllAssetSummaryAsync(assetId, null).ConfigureAwait(false))
                .OrderByDescending(a => a.SummaryYear)
                .ThenByDescending(a => a.SubmitDate)
                .ToList();

            var last3Years = assetSummaries.Select(a => a.SummaryYear).Distinct().OrderByDescending(year => year).Take(3).ToList();

            var summaryList = last3Years.Select(year => assetSummaries.First(s => s.SummaryYear == year)).ToList();

            return summaryList.ToViewResponse();
        }
    }
}
