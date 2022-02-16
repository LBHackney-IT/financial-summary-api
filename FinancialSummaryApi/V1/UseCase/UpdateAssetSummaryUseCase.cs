using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
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

        public Task ExecuteAsync(AssetSummaryResponse response)
            => _gateway.UpdateAsync(response.ToDomain());
    }
}
