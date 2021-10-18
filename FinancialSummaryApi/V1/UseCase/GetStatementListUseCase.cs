using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetStatementListUseCase : IGetStatementListUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public GetStatementListUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<StatementListResponse> ExecuteAsync(Guid targetId, GetStatementListRequest request)
        {
            var statementList =  await _financeSummaryGateway.GetStatementListAsync(targetId, request).ConfigureAwait(false);
            return new StatementListResponse
            {
                Total = statementList.Total,
                Statements = statementList?.Statements?.ToResponse()
            };
        }
    }
}
