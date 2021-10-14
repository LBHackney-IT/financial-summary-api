using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;
using FinancialSummaryApi.V1.Factories;

namespace FinancialSummaryApi.V1.UseCase
{
    public class AddStatementUseCase : IAddStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public AddStatementUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<StatementResponse> ExecuteAsync(AddStatementRequest statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            var domainModel = statement.ToDomain();

            domainModel.Id = Guid.NewGuid();

            await _financeSummaryGateway.AddAsync(domainModel).ConfigureAwait(false);

            return domainModel.ToResponse();
        }
    }
}
