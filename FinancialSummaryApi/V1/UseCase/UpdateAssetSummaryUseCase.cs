using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class UpdateAssetSummaryUseCase : IUpdateAssetSummaryUseCase
    {
        private readonly IFinanceSummaryGateway _gateway;

        public UpdateAssetSummaryUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ExecuteAsync(UpdateAssetSummaryRequest assetSummary)
        {
            var domainModel = assetSummary.ToDomain();

            await _gateway.UpdateAsync(domainModel).ConfigureAwait(false);
        }
    }
}
