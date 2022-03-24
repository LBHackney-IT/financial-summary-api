using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Hackney.Core.DynamoDb;
using System;
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

        public async Task<PagedResult<StatementResponse>> ExecuteAsync(Guid targetId, GetStatementListRequest request)
        {
            if (request.StartDate != DateTime.MinValue && request.EndDate != DateTime.MinValue)
            {
                request.StartDate = request.StartDate.Date.GetDayRange().dayStart;
                request.EndDate = request.EndDate.Date.GetDayRange().dayEnd;
            }

            var statementList = await _financeSummaryGateway.GetPagedStatementsAsync(targetId, request.StartDate, request.EndDate, request.PageSize, request.PaginationToken).ConfigureAwait(false);

            var statementResponseList = _mapper.Map<PagedResult<StatementResponse>>(statementList);
            return statementResponseList;

        }
    }
}
