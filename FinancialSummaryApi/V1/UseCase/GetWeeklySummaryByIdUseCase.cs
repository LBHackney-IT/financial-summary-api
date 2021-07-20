using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetWeeklySummaryByIdUseCase : IGetWeeklySummaryByIdUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public GetWeeklySummaryByIdUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<WeeklySummaryResponse> ExecuteAsync(Guid id)
        {
            var weeklySummary = await _financeSummaryGateway.GetWeeklySummaryByIdAsync(id).ConfigureAwait(false);

            return weeklySummary?.ToResponse();
        }
    }
}
