using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetStatementByIdUseCase
    {
        public Task<StatementResponse> ExecuteAsync(Guid statementId, Guid targetId);
    }
}
