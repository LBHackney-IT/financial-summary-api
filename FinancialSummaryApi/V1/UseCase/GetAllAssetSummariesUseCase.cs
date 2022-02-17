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
            var assetSummaries = (await _financeSummaryGateway.GetAllAssetSummaryAsync(assetId, null).ConfigureAwait(false)).ToViewResponse();

            return assetSummaries.OrderBy(a => a.SummaryYear).ToList();
        }
    }
}
