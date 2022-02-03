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
    public class GetAssetSummaryByIdAndYearUseCase : IGetAssetSummaryByIdAndYearUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        public GetAssetSummaryByIdAndYearUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }
        public async Task<AssetSummaryResponse> ExecuteAsync(Guid assetId, short summaryYear)
        {
            var assetSummary = await _financeSummaryGateway.GetAssetSummaryByIdAndYearAsync(assetId, summaryYear).ConfigureAwait(false);

            return assetSummary?.ToResponse();
        }
    }
}
