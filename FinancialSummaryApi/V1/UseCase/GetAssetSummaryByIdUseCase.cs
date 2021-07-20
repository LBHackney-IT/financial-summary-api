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

        public GetAssetSummaryByIdUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<AssetSummaryResponse> ExecuteAsync(Guid assetId, DateTime submitDate)
        {
            if (submitDate == DateTime.MinValue)
            {
                submitDate = DateTime.UtcNow;
            }
            var assetSummary = await _financeSummaryGateway.GetAssetSummaryByIdAsync(assetId, submitDate).ConfigureAwait(false);

            return assetSummary?.ToResponse();
        }
    }
}
