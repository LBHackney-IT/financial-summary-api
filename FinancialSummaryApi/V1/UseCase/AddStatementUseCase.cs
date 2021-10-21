using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;
using AutoMapper;
using FinancialSummaryApi.V1.Domain;

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

        public async Task<StatementResponse> ExecuteAsync(AddStatementRequest statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            var domainModel = _mapper.Map<Statement>(statement);

            domainModel.Id = Guid.NewGuid();

            await _financeSummaryGateway.AddAsync(domainModel).ConfigureAwait(false);

            return _mapper.Map<StatementResponse>(domainModel);
        }
    }
}
