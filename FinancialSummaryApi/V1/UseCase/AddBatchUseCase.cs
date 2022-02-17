using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddBatchUseCase : IAddBatchUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        public AddBatchUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }
        public async Task<int> ExecuteAsync(IEnumerable<AddAssetSummaryRequest> assetSummaryRequests)
        {
            var assetSummariesList = assetSummaryRequests.ToList().ToDomainList();
            assetSummariesList.ForEach(item =>
            {
                item.Id = Guid.NewGuid();
            });

            var response = await _financeSummaryGateway.AddBatchAsync(assetSummariesList).ConfigureAwait(false);
            return assetSummariesList.Count;
        }
    }
}
