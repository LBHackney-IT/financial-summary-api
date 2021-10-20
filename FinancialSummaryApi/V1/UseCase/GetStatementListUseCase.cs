using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
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
            var startDate = request.StartDate.Date;
            var endDate = request.EndDate.Date;
           
            if (startDate == endDate)
            {
                (startDate, endDate) = new Tuple<DateTime, DateTime>(startDate.Date, endDate.Date.AddHours(23).AddMinutes(59));
            }

            long total = await _financeSummaryGateway.GetStatementsTotalAsync(targetId, startDate, endDate).ConfigureAwait(false);

            var statementList = (total > (request.PageNumber - 1) * request.PageSize)?
                await _financeSummaryGateway.GetPagedStatementsAsync(targetId, startDate, endDate, request.PageSize, request.PageNumber).ConfigureAwait(false) : new List<Statement>();
            
            return new StatementListResponse
            {
                Total = total,
                Statements = statementList?.ToResponse()
            };
        }
    }
}
