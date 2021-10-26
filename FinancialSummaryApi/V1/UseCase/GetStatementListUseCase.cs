using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetStatementListUseCase : IGetStatementListUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IMapper _mapper;

        public GetStatementListUseCase(IFinanceSummaryGateway financeSummaryGateway, IMapper mapper)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _mapper = mapper;
        }

        public async Task<StatementListResponse> ExecuteAsync(Guid targetId, GetStatementListRequest request)
        {
            var startDate = request.StartDate.Date.GetDayRange().dayStart;
            var endDate = request.EndDate.Date.GetDayRange().dayEnd;

            var statementList = await _financeSummaryGateway.GetPagedStatementsAsync(targetId, startDate, endDate, request.PageSize, request.PageNumber).ConfigureAwait(false);

            var statementResponseList = _mapper.Map<List<StatementResponse>>(statementList.Statements);

            return new StatementListResponse
            {
                Total = statementList.Total,
                Statements = statementResponseList
            };
        }
    }
}
