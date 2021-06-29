using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddAssetSummaryUseCase : IAddAssetSummaryUseCase
    {
        private readonly IFinanceSummaryGateway _gateway;
        public AddAssetSummaryUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ExecuteAsync(AddAssetSummaryRequest assetSummary)
        {
            var domainModel = assetSummary.ToDomain();

            domainModel.Id = Guid.NewGuid();

            await _gateway.AddAsync(domainModel).ConfigureAwait(false);
        }
    }
}
