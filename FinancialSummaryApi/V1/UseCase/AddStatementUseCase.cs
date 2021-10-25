using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSummaryApi.V1.Domain;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddStatementUseCase : IAddStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IMapper _mapper;

        public AddStatementUseCase(IFinanceSummaryGateway financeSummaryGateway, IMapper mapper)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _mapper = mapper;
        }

        public async Task<List<StatementResponse>> ExecuteAsync(List<AddStatementRequest> statements)
        {
            if (statements == null)
            {
                throw new ArgumentNullException(nameof(statements));
            }

            var domainModels = _mapper.Map<List<Statement>>(statements);
            foreach(var domainModel in domainModels)
            {
                domainModel.Id = Guid.NewGuid();
            }

            await _financeSummaryGateway.AddRangeAsync(domainModels).ConfigureAwait(false);

            return _mapper.Map<List<StatementResponse>>(domainModels);
        }
    }
}
