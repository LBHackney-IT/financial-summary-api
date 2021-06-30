using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAssetSummaryByIdUseCase : IGetAssetSummaryByIdUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IAssetInfoDbGateway _assetInfoGateway;

        public GetAssetSummaryByIdUseCase(IFinanceSummaryGateway financeSummaryGateway,
            IAssetInfoDbGateway assetInfoGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _assetInfoGateway = assetInfoGateway;
        }

        public async Task<AssetSummaryResponse> ExecuteAsync(Guid assetId, DateTime submitDate)
        {
            if (submitDate == DateTime.MinValue)
            {
                submitDate = DateTime.UtcNow;
            }
            var assetSummary = await _financeSummaryGateway.GetAssetSummaryByIdAsync(assetId, submitDate).ConfigureAwait(false);

            if(assetSummary != null)
            {
                var assetName = await _assetInfoGateway.GetAssetNameByAssetIdAsync(assetId).ConfigureAwait(false);

                assetSummary.AssetName = assetName;
            }

            return assetSummary?.ToResponse();
        }
    }
}
