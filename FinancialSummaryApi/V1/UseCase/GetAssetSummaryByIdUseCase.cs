using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAssetSummaryByIdUseCase : IGetAssetSummaryByIdUseCase
    {
        private IFinanceSummaryGateway _gateway;

        public GetAssetSummaryByIdUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AssetSummaryResponse> ExecuteAsync(Guid assetId)
        {
            var assetSummary = await _gateway.GetAssetSummaryByIdAsync(assetId).ConfigureAwait(false);

            if(assetSummary != null)
            {
                var assetName = await _gateway.GetAssetNameByAssetIdAsync(assetId).ConfigureAwait(false);

                assetSummary.AssetName = assetName;
            }

            return assetSummary?.ToResponse();
        }
    }
}
