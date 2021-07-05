using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddAssetSummaryUseCase : IAddAssetSummaryUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public AddAssetSummaryUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task ExecuteAsync(AddAssetSummaryRequest assetSummary)
        {
            if(assetSummary == null)
            {
                throw new ArgumentNullException(nameof(assetSummary));
            }

            var domainModel = assetSummary.ToDomain();

            domainModel.Id = Guid.NewGuid();

            await _financeSummaryGateway.AddAsync(domainModel).ConfigureAwait(false);
        }
    }
}
