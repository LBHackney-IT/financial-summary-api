using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddRentGroupSummaryListUseCase : IAddRentGroupSummaryListUseCase
    {
        private readonly IFinanceSummaryGateway _gateway;
        public AddRentGroupSummaryListUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<RentGroupSummaryResponse>> ExecuteAsync(List<AddRentGroupSummaryRequest> summaryRequests)
        {
            if (summaryRequests == null || summaryRequests.Count == 0)
            {
                throw new ArgumentNullException(nameof(summaryRequests));
            }

            var domainModels = summaryRequests.Select(x => x.ToDomain()).ToList();
            foreach (var model in domainModels)
            {
                model.Id = Guid.NewGuid();
            }

            await _gateway.AddRangeAsync(domainModels).ConfigureAwait(false);

            return domainModels.Select(x => x.ToResponse()).ToList();
        }
    }
}
