using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAllRentGroupSummariesUseCase : IGetAllRentGroupSummariesUseCase
    {
        private IFinanceSummaryGateway _gateway;

        public GetAllRentGroupSummariesUseCase(IFinanceSummaryGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<RentGroupSummaryResponse>> ExecuteAsync()
        {
            var rentGroups = await _gateway.GetAllRentGroupSummaryAsync().ConfigureAwait(false);

            return rentGroups?.Select(r => r.ToResponse()).ToList();
        }
    }
}
