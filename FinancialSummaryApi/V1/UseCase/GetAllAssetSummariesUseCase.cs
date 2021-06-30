using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAllAssetSummariesUseCase : IGetAllAssetSummariesUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IAssetInfoDbGateway _assetInfoGateway;

        public GetAllAssetSummariesUseCase(IFinanceSummaryGateway financeSummaryGateway,
            IAssetInfoDbGateway assetInfoGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _assetInfoGateway = assetInfoGateway;
        }

        public async Task<List<AssetSummaryResponse>> ExecuteAsync(DateTime submitDate)
        {
            if (submitDate == DateTime.MinValue)
            {
                submitDate = DateTime.UtcNow;
            }

            var assetSummaries = (await _financeSummaryGateway.GetAllAssetSummaryAsync(submitDate).ConfigureAwait(false)).ToResponse();

            foreach(var asset in assetSummaries)
            {
                var assetInfo = await _assetInfoGateway.GetAssetInfoAsync(asset.TargetId).ConfigureAwait(false);
                asset.AssetName = assetInfo?.AssetName;
            }

            return assetSummaries;
        }
    }
}
