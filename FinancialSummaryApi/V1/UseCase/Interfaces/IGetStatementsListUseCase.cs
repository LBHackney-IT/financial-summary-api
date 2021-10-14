using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetStatementsListUseCase
    {
        Task<GetStatementListResponse> ExecuteAsync(Guid targetId, GetStatementListRequest request);
    }
}
