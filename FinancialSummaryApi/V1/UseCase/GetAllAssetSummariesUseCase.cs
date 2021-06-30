using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAllAssetSummariesUseCase : IGetAllAssetSummariesUseCase
    {
        private readonly IFinanceSummaryGateway _gateway;

        public GetAllAssetSummariesUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<AssetSummaryResponse>> ExecuteAsync()
        {
            var assetSummaries = (await _gateway.GetAllAssetSummaryAsync().ConfigureAwait(true)).ToResponse();

            // ToDo: refactor this
            assetSummaries.ForEach(async(a) => 
            {
                a.AssetName = await _gateway.GetAssetNameByTenureIdAsync(a.TargetId).ConfigureAwait(false);
            });

            return assetSummaries;
        }
    }
}
