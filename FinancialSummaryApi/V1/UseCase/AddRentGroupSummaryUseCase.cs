using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
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

        public async Task<RentGroupSummaryResponse> ExecuteAsync(AddRentGroupSummaryRequest rentGroupSummary)
        {
            if (rentGroupSummary == null)
            {
                throw new ArgumentNullException(nameof(rentGroupSummary));
            }

            var domainModel = rentGroupSummary.ToRentGroupDomain();

            domainModel.Id = Guid.NewGuid();

            await _gateway.AddAsync(domainModel).ConfigureAwait(false);

            return domainModel.ToResponse();
        }
    }
}
