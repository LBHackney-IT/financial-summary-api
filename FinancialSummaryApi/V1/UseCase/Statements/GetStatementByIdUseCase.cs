using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetStatementByIdUseCase : IGetStatementByIdUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IMapper _mapper;

        public GetStatementByIdUseCase(IFinanceSummaryGateway financeSummaryGateway, IMapper mapper)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _mapper = mapper;
        }

        public async Task<StatementResponse> ExecuteAsync(Guid statementId, Guid targetId)
        {
            var statement = await _financeSummaryGateway.GetStatementByIdAsync(statementId, targetId).ConfigureAwait(false);

            return _mapper.Map<StatementResponse>(statement);
        }
    }
}
