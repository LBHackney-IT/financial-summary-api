using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetStatementsListUseCase : IGetStatementsListUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public GetStatementsListUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<GetStatementListResponse> ExecuteAsync(Guid targetId, GetStatementListRequest request)
        {
            return await _financeSummaryGateway.GetStatementsListAsync(targetId, request).ConfigureAwait(false);
        }
    }
}
