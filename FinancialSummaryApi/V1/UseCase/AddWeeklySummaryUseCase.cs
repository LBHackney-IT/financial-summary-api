using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddWeeklySummaryUseCase : IAddWeeklySummaryUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public AddWeeklySummaryUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }
        public async Task<WeeklySummaryResponse> ExecuteAsync(AddWeeklySummaryRequest weeklySummary)
        {
            if (weeklySummary == null)
            {
                throw new ArgumentNullException(nameof(weeklySummary));
            }

            var domainModel = weeklySummary.ToDomain();

            domainModel.Id = Guid.NewGuid();

            await _financeSummaryGateway.AddAsync(domainModel).ConfigureAwait(false);

            return domainModel.ToResponse();
        }
    }
}
