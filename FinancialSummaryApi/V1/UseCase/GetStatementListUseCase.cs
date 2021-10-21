using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.Infrastructure;
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
            var startDate = request.StartDate.Date;
            var endDate = request.EndDate.Date;
           
            if (startDate == endDate)
            {
                (startDate, endDate) = startDate.GetDayRange();
            }

            long total = await _financeSummaryGateway.GetStatementsTotalAsync(targetId, startDate, endDate).ConfigureAwait(false);

            var statementList = (total > (request.PageNumber - 1) * request.PageSize)?
                await _financeSummaryGateway.GetPagedStatementsAsync(targetId, startDate, endDate, request.PageSize, request.PageNumber).ConfigureAwait(false) : new List<Statement>();
            var statementListResponse = _mapper.Map<List<StatementResponse>>(statementList);

            return new StatementListResponse
            {
                Total = total,
                Statements = statementListResponse
            };
        }
    }
}
