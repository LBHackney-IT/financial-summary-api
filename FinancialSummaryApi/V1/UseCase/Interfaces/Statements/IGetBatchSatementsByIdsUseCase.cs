using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces.Statements
{
    public interface IGetBatchSatementsByIdsUseCase
    {
        Task<List<StatementResponse>> ExecuteAsync(List<Guid> targetIds);
    }
}
