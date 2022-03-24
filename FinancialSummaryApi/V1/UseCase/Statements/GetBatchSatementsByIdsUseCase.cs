using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces.Statements;

namespace FinancialSummaryApi.V1.UseCase.Statements
{
    public class GetBatchSatementsByIdsUseCase : IGetBatchSatementsByIdsUseCase

    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IMapper _mapper;

        public GetBatchSatementsByIdsUseCase(IFinanceSummaryGateway financeSummaryGateway, IMapper mapper)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _mapper = mapper;
        }

        public async Task<List<StatementResponse>> ExecuteAsync(List<Guid> targetIds)
        {
            var statements = await _financeSummaryGateway.GetBatchStatementsByIds(targetIds).ConfigureAwait(false);

            return _mapper.Map<List<StatementResponse>>(statements);
        }
    }
}
