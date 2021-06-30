using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddRentGroupSummaryUseCase : IAddRentGroupSummaryUseCase
    {
        private readonly IFinanceSummaryGateway _gateway;
        public AddRentGroupSummaryUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ExecuteAsync(AddRentGroupSummaryRequest assetSummary)
        {
            var domainModel = assetSummary.ToRentGroupDomain();

            domainModel.Id = Guid.NewGuid();

            await _gateway.AddAsync(domainModel).ConfigureAwait(false);
        }
    }
}
