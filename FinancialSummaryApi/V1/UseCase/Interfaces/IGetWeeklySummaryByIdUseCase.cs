using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetWeeklySummaryByIdUseCase
    {
        Task<WeeklySummaryResponse> ExecuteAsync(Guid targetId, Guid id);
    }
}
