using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using Hackney.Core.DynamoDb;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetStatementListUseCase
    {
        Task<PagedResult<StatementResponse>> ExecuteAsync(Guid targetId, GetStatementListRequest request);
    }
}
